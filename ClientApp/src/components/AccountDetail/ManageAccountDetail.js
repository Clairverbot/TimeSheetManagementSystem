import React, { Component } from 'react'
import { Header, Icon, Table, Button, Container,Confirm, Grid, Menu, Segment } from 'semantic-ui-react';
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
            selectedId:-1,
            showDeleteDialog:false
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
                        selectedAccount: response.data[0]
                        //loading: false,
                    });
            }
            );
    }
    handleAddDetailClick(e, id, callback,buttonName) {
        callback(id)
        switch (buttonName){
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
    renderAccountDetailsTable(item) {
        console.log(item.accountDetails)
        return (
            <AuthContext.Consumer>
                {({ selectTask }) => (
                    item.accountDetails.length === 0 ?
                        <Grid centered rows='2'>
                            <Grid.Row centered>
                                <Button circular icon='plus' positive size="large"
                                    onClick={(e) => this.handleAddDetailClick(e, item.customerAccountId, selectTask,'create')} />
                            </Grid.Row>
                            <Grid.Row centered>
                                <Header icon as='h4' textAlign='center'>
                                    Add Your First Account Detail
                	</Header>
                            </Grid.Row>
                        </Grid>
                        :
                        <Table sortable celled selectable size='small'>
                            <Table.Header>
                            <Table.Row>
							<Table.HeaderCell colSpan='13'>
											<Button icon labelPosition='left' floated='right' positive size="mini"
                                            onClick={(e) => this.handleAddDetailClick(e, item.customerAccountId, selectTask,'create')}
                                            >
												<Icon name='plus' />
												Add Account Rate
                            </Button>
							</Table.HeaderCell>
						</Table.Row>
                                <Table.Row>
                                    <Table.HeaderCell>#</Table.HeaderCell>
                                    <Table.HeaderCell>Day of Week</Table.HeaderCell>
                                    <Table.HeaderCell>Start Time</Table.HeaderCell>
                                    <Table.HeaderCell>End Time</Table.HeaderCell>
                                    <Table.HeaderCell>Effective Start Date</Table.HeaderCell>
                                    <Table.HeaderCell>Effective End Date</Table.HeaderCell>
                                    <Table.HeaderCell>Is Visible</Table.HeaderCell>
                                    <Table.HeaderCell>Actions</Table.HeaderCell>
                                </Table.Row>
                            </Table.Header>
                            <Table.Body>
                            {item.accountDetails.map((element, index) => {
                                    return(
                                    <Table.Row>
                                        <Table.Cell>{index+1}</Table.Cell>
                                        <Table.Cell>{moment().isoWeekday(element.dayOfWeekNumber).format('dddd')}</Table.Cell>
                                        <Table.Cell>{moment.utc(element.startTimeInMinutes, "HH:mm:ss").format('hh:mm A')}</Table.Cell>
                                        <Table.Cell>{moment.utc(element.endTimeInMinutes, "HH:mm:ss").format('hh:mm A')}</Table.Cell>
                                        <Table.Cell>{element.effectiveStartDate.slice(0, -9)}</Table.Cell>
                                        <Table.Cell>{element.effectiveEndDate.slice(0, -9)}</Table.Cell>
                                        <Table.Cell>{element.isVisible
                                            ? <Icon name="check" />
                                            : <Icon name="close" />}</Table.Cell>
                                        <Table.Cell>
                                            <Button.Group size="mini">
                                                <Button icon='edit'
                                                    positive
                                                onClick={(event) => this.handleAddDetailClick(event, element.accountDetailId, selectTask,'update')} 
                                                />
                                                <Button
                                                    negative
                                                    icon='trash'
                                                onClick={this.deleteDialog.bind(this, element.accountDetailId)}
                                                />
                                            </Button.Group>
                                        </Table.Cell>
                                    </Table.Row>
                                    )
                            })
                        }
                        </Table.Body>
                        </Table>
                )}
            </AuthContext.Consumer>
        )
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
                        <Grid.Column width={4}>
                            <Menu fluid vertical tabular>
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

                        <Grid.Column stretched width={12}>
                            <Segment>
                                {this.renderAccountDetailsTable(this.state.selectedAccount)}

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
