import React, { Component } from 'react'
import { Header, Icon, Loader, Table, Button, Container, Confirm, Grid, Menu, Segment } from 'semantic-ui-react';
import AuthContext from './../../AuthContext';
import axios from 'axios'
import _ from 'lodash'
import moment from 'moment'

export class ManageAccountDetail extends Component {
    displayName = ManageAccountDetail.name
    constructor(props) {
        super(props);
        this.state = {
            accountDetails: [],
            selectedAccount: 0,
            activeItem: '',
            //delete
            selectedId: -1,
            showDeleteDialog: false,
            //hide account rate
            rateVisibility: true,

        }

        var config = {
            withCredentials: false,
            headers: {
                'Authorization': 'Bearer ' + this.props.token
            }
        }
        axios.get('/api/AccountDetails', config)
            .then(response => {
                if (response.data.length !== 0)
                    this.setState({
                        accountDetails: response.data,
                        activeItem: response.data[0].accountName,
                        selectedAccount: response.data[0],
                    });
                console.log(response.data)
            }
            );
    }
    handleAddDetailClick(e, id, callback, buttonName) {
        callback(id)
        switch (buttonName) {
            case 'create':
                this.props.history.push("./Create")
                break;
            case 'update':
                this.props.history.push("./Update")

        }
    }
    deleteDialog(accountDetailId, e) {
        this.setState(state => ({
            selectedId: accountDetailId,
            showDeleteDialog: !state.showDeleteDialog,
        }));

    }
    deleteAccountDetail(id, e) {
        fetch('/api/AccountDetails/' + id, {
            method: 'DELETE',
            withCredentials: false,
            headers: {
                'Authorization': 'Bearer ' + this.props.token
            },
        }
        )
            .then(window.location.href = "./AccountDetail/Manage")
        this.setState(state => ({
            showDeleteDialog: false
        }));

    }
    renderAccountDetailsTable(item, rowSpan) {
        return (
            <AuthContext.Consumer>
                {({ selectTask }) => (
                    item.accountDetails.length === 0 ?
                        <Grid centered rows='2'>
                            <Grid.Row centered>
                                <Button circular icon='plus' positive size="large"
                                    onClick={(e) => this.handleAddDetailClick(e, item.customerAccountId, selectTask, 'create')} />
                            </Grid.Row>
                            <Grid.Row centered>
                                <Header icon as='h4' textAlign='center'>
                                    Add Your First Account Detail
                	</Header>
                            </Grid.Row>
                        </Grid>
                        :
                        <Table sortable celled selectable size='small' compact='very'>
                            <Table.Header>
                                <Table.Row>
                                    <Table.HeaderCell colSpan='11'>
                                        <Button icon labelPosition='left' floated='right' positive size="mini"
                                            onClick={(e) => this.handleAddDetailClick(e, item.customerAccountId, selectTask, 'create')}
                                        >
                                            <Icon name='plus' />
                                            Add Account Detail
                            </Button>
                                    </Table.HeaderCell>
                                </Table.Row>
                                <Table.Row>
                                    <Table.HeaderCell rowSpan={2}>#</Table.HeaderCell>
                                    <Table.HeaderCell rowSpan={2}>Day of Week</Table.HeaderCell>
                                    <Table.HeaderCell rowSpan={2}>Start Time</Table.HeaderCell>
                                    <Table.HeaderCell rowSpan={2}>End Time</Table.HeaderCell>
                                    <Table.HeaderCell rowSpan={2}>{'Effective Start Date'}</Table.HeaderCell>
                                    <Table.HeaderCell rowSpan={2}>Effective End Date</Table.HeaderCell>
                                    <Table.HeaderCell rowSpan={2}>Is Visible</Table.HeaderCell>
                                    <Table.HeaderCell rowSpan={2}>Actions</Table.HeaderCell>
                                    <Table.HeaderCell colSpan={3} hidden={!this.state.rateVisibility}>Account Rates</Table.HeaderCell>
                                </Table.Row>
                                <Table.Row hidden={!this.state.rateVisibility}>
                                    <Table.HeaderCell>Rate Per Hour</Table.HeaderCell>
                                    <Table.HeaderCell>Effective Start Date</Table.HeaderCell>
                                    <Table.HeaderCell>Effective End Date</Table.HeaderCell>
                                </Table.Row>
                            </Table.Header>
                            <Table.Body>
                                {item.accountDetails.map((element, index) => {
                                    return (
                                        [
                                            <Table.Row warning={!element.isVisible}>
                                                <Table.Cell rowSpan={this.state.rateVisibility ? rowSpan : 1}>{index + 1}</Table.Cell>
                                                <Table.Cell rowSpan={this.state.rateVisibility ? rowSpan : 1}>{moment().isoWeekday(element.dayOfWeekNumber).format('dddd')}</Table.Cell>
                                                <Table.Cell rowSpan={this.state.rateVisibility ? rowSpan : 1}>{moment.utc(element.startTimeInMinutes, "HH:mm:ss").format('hh:mm A')}</Table.Cell>
                                                <Table.Cell rowSpan={this.state.rateVisibility ? rowSpan : 1}>{moment.utc(element.endTimeInMinutes, "HH:mm:ss").format('hh:mm A')}</Table.Cell>
                                                <Table.Cell rowSpan={this.state.rateVisibility ? rowSpan : 1}>{element.effectiveStartDate.slice(0, -9)}</Table.Cell>
                                                <Table.Cell rowSpan={this.state.rateVisibility ? rowSpan : 1}>{element.effectiveEndDate.slice(0, -9)}</Table.Cell>
                                                <Table.Cell rowSpan={this.state.rateVisibility ? rowSpan : 1}>{element.isVisible
                                                    ? <Icon name="check" />
                                                    : <Icon name="close" />}</Table.Cell>
                                                <Table.Cell rowSpan={this.state.rateVisibility ? rowSpan : 1}>
                                                    <Button.Group size="mini">
                                                        <Button icon='edit'
                                                            positive
                                                            onClick={(event) => this.handleAddDetailClick(event, element.accountDetailId, selectTask, 'update')}
                                                        />
                                                        <Button
                                                            negative
                                                            icon='trash'
                                                            onClick={this.deleteDialog.bind(this, element.accountDetailId)}
                                                        />
                                                    </Button.Group>
                                                </Table.Cell>
                                                {this.renderFirstAccountRate(element, item.customerAccountId)}
                                            </Table.Row>,
                                            this.renderOtherAccountRate(element, item.customerAccountId),
                                            this.renderMissingAccountRate(index, item.customerAccountId)
                                        ]
                                    )
                                })
                                }

                            </Table.Body>

                            <Table.Footer>
                                <Table.Row>
                                    <Table.HeaderCell colSpan='11'>
                                        <Button
                                            icon
                                            labelPosition='left'
                                            floated='right'
                                            positive size="mini"
                                            onClick={(e) => this.setState({ rateVisibility: !this.state.rateVisibility })}>
                                            <Icon name={this.state.rateVisibility ? 'hide' : 'unhide'} />
                                            {this.state.rateVisibility ? 'Hide Account Rate' : 'Show Account Rate'}
                                        </Button>
                                    </Table.HeaderCell>
                                </Table.Row>
                            </Table.Footer>
                        </Table>
                )}
            </AuthContext.Consumer>
        )
    }
    renderFirstAccountRate(accountDetail, customerAccountId) {
        var CustomerAccount = {}
        this.state.accountDetails.forEach(element => {
            if (element.customerAccountId === customerAccountId)
                CustomerAccount = element
        });
        const accRate = CustomerAccount.accountRate[0]
        if (moment(accountDetail.effectiveStartDate).isBetween(accRate.effectiveStartDate, accRate.effectiveEndDate)
            || moment(accountDetail.effectiveEndDate).isBetween(accRate.effectiveStartDate, accRate.effectiveEndDate)
            || moment(accountDetail.effectiveStartDate).isSame(accRate.effectiveStartDate)
            || moment(accountDetail.effectiveEndDate).isSame(accRate.effectiveEndDate)
            || moment(accRate.effectiveStartDate).isBetween(accountDetail.effectiveStartDate, accountDetail.effectiveEndDate)
            || moment(accRate.effectiveEndDate).isBetween(accountDetail.effectiveStartDate, accountDetail.effectiveEndDate)
        )
            return (
                [
                    <Table.Cell hidden={!this.state.rateVisibility}>{accRate.ratePerHour}</Table.Cell>,
                    <Table.Cell hidden={!this.state.rateVisibility}>{accRate.effectiveStartDate.slice(0, -9)}</Table.Cell>,
                    <Table.Cell hidden={!this.state.rateVisibility}>{accRate.effectiveEndDate.slice(0, -9)}</Table.Cell>
                ]
            )
    }
    renderOtherAccountRate(accountDetail, customerAccountId) {
        var CustomerAccount = {}
        this.state.accountDetails.forEach(element => {
            if (element.customerAccountId === customerAccountId)
                CustomerAccount = element
        });
        const accRate = CustomerAccount.accountRate


        return accRate.map((element, index) => {
            return (
                index === 0 ?
                    null
                    :
                    moment(accountDetail.effectiveStartDate).isBetween(element.effectiveStartDate, element.effectiveEndDate)
                        || moment(accountDetail.effectiveEndDate).isBetween(element.effectiveStartDate, element.effectiveEndDate)
                        || moment(accountDetail.effectiveStartDate).isSame(element.effectiveStartDate)
                        || moment(accountDetail.effectiveEndDate).isSame(element.effectiveEndDate)
                        || moment(accRate.effectiveStartDate).isBetween(accountDetail.effectiveStartDate, accountDetail.effectiveEndDate)
                        || moment(accRate.effectiveEndDate).isBetween(accountDetail.effectiveStartDate, accountDetail.effectiveEndDate) ?
                        <Table.Row hidden={!this.state.rateVisibility}>
                            <Table.Cell>{element.ratePerHour}</Table.Cell>
                            <Table.Cell>{element.effectiveStartDate.slice(0, -9)}</Table.Cell>
                            <Table.Cell>{element.effectiveEndDate.slice(0, -9)}</Table.Cell>
                        </Table.Row>
                        :
                        <Table.Row></Table.Row>
            )
        })
    }
    renderMissingAccountRate(accountDetailId, customerAccountId) {
        var CustomerAccount = {}
        this.state.accountDetails.forEach(element => {
            if (element.customerAccountId === customerAccountId)
                CustomerAccount = element
        });
        const accRate = CustomerAccount.accountRate
        const accDetail = CustomerAccount.accountDetails[accountDetailId]
        var rows = []
        if (moment(accRate[0].effectiveStartDate.slice(0, -9))
            .isAfter(accDetail.effectiveStartDate.slice(0, -9))) {
            rows.push(
                <Table.Row error hidden={!this.state.rateVisibility}>
                    <Table.Cell>NA</Table.Cell>
                    <Table.Cell>{accDetail.effectiveStartDate.slice(0, -9)}</Table.Cell>
                    <Table.Cell>{accRate[0].effectiveStartDate.slice(0, -9)}</Table.Cell>
                </Table.Row>
            )
        }
        else { rows.push(<Table.Row></Table.Row>) }
        if (moment(accRate[accRate.length - 1].effectiveEndDate.slice(0, -9))
            .isBefore(accDetail.effectiveEndDate.slice(0, -9))) {
            rows.push(
                <Table.Row error hidden={!this.state.rateVisibility}>
                    <Table.Cell>NA</Table.Cell>
                    <Table.Cell>{accRate[accRate.length - 1].effectiveEndDate.slice(0, -9)}</Table.Cell>
                    <Table.Cell>{accDetail.effectiveEndDate.slice(0, -9)}</Table.Cell>
                </Table.Row>
            )
        }
        else { rows.push(<Table.Row></Table.Row>) }

        return rows


    }




    render() {
        return (
            <Container fluid>
                <Header as='h2'>Manage Account Detail</Header>

                {this.state.accountDetails.length === 0 ?
                    <Grid>
                        <Segment>
                            <br />
                            <Grid.Row centered>
                                <Button circular icon='plus' positive size="large" href="../CustomerAccount/Create" />
                            </Grid.Row>
                            <Grid.Row centered>
                                <Header icon as='h4' textAlign='center'>
                                    Add Your First Customer Account
                	</Header>
                            </Grid.Row>
                            <br />
                        </Segment>
                    </Grid>
                    :
                    <Grid>
                        <Grid.Column width={2}>
                            <Menu vertical tabular>
                                {this.state.accountDetails.map((accountDetail, index) => {
                                    return (
                                        <Menu.Item
                                            name={accountDetail.accountName}
                                            active={this.state.activeItem === accountDetail.accountName}
                                            onClick={(e, { name }) => {
                                                this.setState({ activeItem: name, selectedAccount: accountDetail })
                                            }} />
                                    )
                                })}
                            </Menu>
                        </Grid.Column>

                        <Grid.Column stretched width={14}>
                            <Segment>
                                {this.renderAccountDetailsTable(this.state.selectedAccount, this.state.selectedAccount.accountRate.length + 2)}
                            </Segment>
                        </Grid.Column>
                    </Grid>
                }
                <Confirm
                    open={this.state.showDeleteDialog}
                    confirmButton='Yes'
                    header={'Are you sure you want to delete ?'}
                    content='This action cannot be undone'
                    onCancel={this.deleteDialog}
                    onConfirm={this.deleteAccountDetail.bind(this, this.state.selectedId)}
                    size='mini'
                />
            </Container>
        )
    }
}
