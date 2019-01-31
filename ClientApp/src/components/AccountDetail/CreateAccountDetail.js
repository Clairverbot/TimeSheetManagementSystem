import React, { Component } from 'react';
import AuthContext from './../../AuthContext';
import { Segment, Header, Form, Checkbox, Button, Dropdown, Table, Grid, Divider } from 'semantic-ui-react';
import axios from 'axios';
import moment from 'moment';

export class CreateAccountDetail extends Component {
    displayName = CreateAccountDetail.name
    constructor(props) {
        super(props)
        this.state = {
            id: props.selectedTaskIdForUpdate,
            nowDate: new Date(),
            startTime: {
                hour: 1,
                minute: '00',
                ampm: 'PM'
            },
            endTime: {
                hour: 2,
                minute: '00',
                ampm: 'PM'
            },
            accountDetail: {
                dayOfWeekNumber: 1,
                startTimeInMinutes: '',
                endTimeInMinutes: '',
                effectiveStartDate: new Date(),
                effectiveEndDate: null,
                isVisible: true
            },
            existingDetails: [],
            minuteDropdown: [{ key: 0, text: '00', value: '00' }, { key: 1, text: '15', value: '15' }, { key: 2, text: '30', value: '30' }, { key: 3, text: '45', value: '45' }],
            ampmDropdown: [{ key: 0, text: 'AM', value: 'AM' }, { key: 1, text: 'PM', value: 'PM' }]
        }
        fetch('/api/AccountDetails/GetByCustAcc/' + this.state.id, {
            withCredentials: false,
            headers: {
                'Authorization': 'Bearer ' + this.props.token
            },
        })
            .then(response => response.json())
            .then(data => { console.log(data); this.setState({ existingDetails: data }) })

    }


    getDropdownValue() {
        let dayOfWeekDropdown = []
        for (let i = 1; i <= 7; i++) {
            dayOfWeekDropdown.push({
                key: i,
                text: moment().isoWeekday(i).format('dddd'),
                value: i
            })
        }
        return dayOfWeekDropdown;
    }
    getTimeHourDropdown() {
        let timeHourDropdown = []
        for (let i = 1; i <= 12; i++) {
            timeHourDropdown.push({
                key: i,
                text: i,
                value: i
            })
        }
        return timeHourDropdown;
    }
    async submitAccountDetailForm(event, token) {
        var ST = this.state.startTime.ampm === 'AM' ?
            this.state.startTime.hour + ':' + this.state.startTime.minute + ':00'
            : (this.state.startTime.hour + 12) + ':' + this.state.startTime.minute + ':00';
        var ET = this.state.endTime.ampm === 'AM' ?
            this.state.endTime.hour + ':' + this.state.endTime.minute + ':00'
            : (this.state.endTime.hour + 12) + ':' + this.state.endTime.minute + ':00'
        await this.setState({
            accountDetail: {
                ...this.state.accountDetail,
                startTimeInMinutes: ST, endTimeInMinutes: ET
            }
        })
        try {
            var config = {
                withCredentials: false,
                headers: {
                    'Content-Type': 'multipart/form-data',
                    'Authorization': 'Bearer ' + token
                }
            }

            var form_data = new FormData();
            for (var key in this.state.accountDetail) {
                form_data.append(key, this.state.accountDetail[key]);
            }
            console.log(this.state.accountDetail)
            axios.post('/api/AccountDetails/' + this.state.id, form_data, config)
        }
        catch (res) {
            console.log(res)
        }
    }
    render() {
        return (
            <Segment padded="very">
                <AuthContext.Consumer>
                    {({ isAuth, token }) => (
                        <Grid columns={2}  relaxed='very' stackable >
                        <Divider vertical/>
                        <Grid.Column>
                            <Header as='h3'>Create Account Detail</Header>
                            <Form>
                                <Form.Field >
                                    <Form.Input
                                        required={true}
                                        label='Week Day Name'
                                    >
                                        <Dropdown
                                            onChange={(e, { value }) => {
                                                this.setState({ accountDetail: { ...this.state.accountDetail, dayOfWeekNumber: value } })
                                            }
                                            }
                                            value={this.getDropdownValue()[this.state.accountDetail.dayOfWeekNumber - 1].value}
                                            options={this.getDropdownValue()}
                                            selection
                                        >
                                        </Dropdown>
                                    </Form.Input>
                                </Form.Field>
                                <Form.Field>
                                    <Form.Input
                                        required={true}
                                        label='Lesson Start Time'
                                    >
                                        <Dropdown
                                            compact
                                            options={this.getTimeHourDropdown()}
                                            selection
                                            value={this.state.startTime.hour}
                                            onChange={(e, { value }) => this.setState({ startTime: { ...this.state.startTime, hour: value } })}
                                        />
                                        <Dropdown
                                            compact
                                            options={this.state.minuteDropdown}
                                            selection
                                            value={this.state.startTime.minute}
                                            onChange={(e, { value }) => this.setState({ startTime: { ...this.state.startTime, minute: value } })}
                                        />
                                        <Dropdown
                                            compact
                                            options={this.state.ampmDropdown}
                                            selection
                                            value={this.state.startTime.ampm}
                                            onChange={(e, { value }) => this.setState({ startTime: { ...this.state.startTime, ampm: value } })}
                                        />
                                    </Form.Input>
                                </Form.Field>
                                <Form.Field>
                                    <Form.Input
                                        required={true}
                                        label='Lesson End Time'
                                    >
                                        <Dropdown
                                            compact
                                            options={this.getTimeHourDropdown()}
                                            selection
                                            value={this.state.endTime.hour}
                                            onChange={(e, { value }) => this.setState({ endTime: { ...this.state.endTime, hour: value } })}
                                        />
                                        <Dropdown
                                            compact
                                            options={this.state.minuteDropdown}
                                            selection
                                            value={this.state.endTime.minute}
                                            onChange={(e, { value }) => this.setState({ endTime: { ...this.state.endTime, minute: value } })}
                                        />
                                        <Dropdown
                                            compact
                                            options={this.state.ampmDropdown}
                                            selection
                                            value={this.state.endTime.ampm}
                                            onChange={(e, { value }) => this.setState({ endTime: { ...this.state.endTime, ampm: value } })}
                                        />
                                    </Form.Input>
                                </Form.Field>
                                <Form.Field
                                    inline>
                                    <Form.Input
                                        fluid
                                        required={true}
                                        type='date'
                                        onChange={(e, { value }) => this.setState({ accountDetail: { ...this.state.accountDetail, effectiveStartDate: value } })}
                                        label='Effective Start Date'
                                        value={this.state.accountDetail.effectiveStartDate}
                                        min={this.state.nowDate}
                                        error={this.state.showError} />
                                </Form.Field>
                                <Form.Field
                                    inline
                                    error={this.state.showError}>
                                    <Form.Input
                                        fluid
                                        type='date'
                                        onChange={(e, { value }) => this.setState({ accountDetail: { ...this.state.accountDetail, effectiveEndDate: value } })}
                                        label='Effective End Date'
                                        value={this.state.endDate}
                                        min={this.state.startDate} />
                                </Form.Field>
                                <Form.Field inline>
                                    <label>Visibility</label>
                                    <Checkbox
                                        onChange={(e, { value }) => this.setState({ accountDetail: { ...this.state.accountDetail, isVisible: value } })}
                                        toggle
                                        checked={this.state.accountDetail.isVisible}
                                    />
                                </Form.Field>
                                <Button positive floated="right"
                                    onClick={(event) => this.submitAccountDetailForm(event, token)}
                                >Create</Button>
                                <Button type='clear' floated="right" onClick={(e) => this.props.history.goBack()}>Cancel</Button>
                            </Form>
                            </Grid.Column>
                            <Grid.Column>
                            <Table celled selectable size='small' compact='very'>
                                <Table.Header>
                                    <Table.Row>
                                        <Table.HeaderCell colSpan='6'>Existing Account Details</Table.HeaderCell>
                                    </Table.Row>
                                    <Table.Row>
                                        <Table.HeaderCell>#</Table.HeaderCell>
                                        <Table.HeaderCell>Day Of Week</Table.HeaderCell>
                                        <Table.HeaderCell>Start Time</Table.HeaderCell>
                                        <Table.HeaderCell>End Time</Table.HeaderCell>
                                        <Table.HeaderCell>Effective Start Date</Table.HeaderCell>
                                        <Table.HeaderCell>Effective End Date</Table.HeaderCell>
                                    </Table.Row>
                                    {this.state.existingDetails.map((element,index)=>{
                                        return(
                                        <Table.Row>
                                            <Table.Cell>{index+1}</Table.Cell>
                                            <Table.Cell>{moment().isoWeekday(element.dayOfWeekNumber).format('dddd')}</Table.Cell>
                                            <Table.Cell>{moment.utc(element.startTimeInMinutes, "HH:mm:ss").format('hh:mm A')}</Table.Cell>
                                            <Table.Cell>{moment.utc(element.endTimeInMinutes, "HH:mm:ss").format('hh:mm A')}</Table.Cell>
                                            <Table.Cell>{element.effectiveStartDate.slice(0,-9)}</Table.Cell>
                                            <Table.Cell>{element.effectiveEndDate.slice(0,-9)}</Table.Cell>
                                        </Table.Row>
                                        )
                                    })
                                }
                                </Table.Header>
                            </Table>
                            </Grid.Column>
                        </Grid>
                    )}
                </AuthContext.Consumer>
            </Segment>
        )
    }
}