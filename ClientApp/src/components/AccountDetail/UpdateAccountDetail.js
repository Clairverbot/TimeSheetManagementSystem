import React, { Component } from 'react';
import AuthContext from './../../AuthContext';
import { Segment, Header, Form, Checkbox, Button, Dropdown } from 'semantic-ui-react';
import axios from 'axios';
import moment from 'moment';

export class UpdateAccountDetail extends Component {
    displayName = UpdateAccountDetail.name
    constructor(props){
        super(props)
        this.state={
            id:props.selectedTaskIdForUpdate,
            loading:true,
            startTime:{
                hour:null,
                minute:null,
                ampm:null
            },
            endTime:{
                hour:null,
                minute:null,
                ampm:null
            },
            accountDetail: {
            },
            minuteDropdown : [{key:0,text:'00',value:'00'},{key:1,text:'15',value:'15'},{key:2,text:'30',value:'30'},{key:3,text:'45',value:'45'}],
            ampmDropdown : [{key:0,text:'AM',value:'AM'},{key:1,text:'PM',value:'PM'}]
        }
        axios.get('/api/AccountDetails/'+this.state.id,{
            withCredentials:false,
            headers:{
                'Authorization': 'Bearer ' + this.props.token
            }
        }).then(response=>{
            this.setState(prevState =>({
                accountDetail:
                {
                        ...prevState.accountDetail,
                        dayOfWeekNumber:response.data.dayOfWeekNumber,
                        startTimeInMinutes:response.data.startTimeInMinutes,
                        endTimeInMinutes:response.data.endTimeInMinutes,
                        effectiveStartDate:response.data.effectiveStartDate.slice(0,-9),
                        effectiveEndDate:response.data.effectiveEndDate.slice(0,-9),
                        isVisible:response.data.isVisible,
                        customerAccountid:response.data.customerAccountId
                    },
                    startTime:{
                        ...prevState.startTime,
                        hour:parseInt(response.data.startTimeInMinutes.slice(0,2))>12?parseInt(response.data.startTimeInMinutes.slice(0,2))-12:parseInt(response.data.startTimeInMinutes.slice(0,2)),
                        minute:response.data.startTimeInMinutes.slice(3,5),
                        ampm:parseInt(response.data.startTimeInMinutes.slice(0,2))>12?'PM':'AM'
                    },
                    endTime:{
                        ...prevState.endTime,
                        hour:parseInt(response.data.endTimeInMinutes.slice(0,2))>12?parseInt(response.data.endTimeInMinutes.slice(0,2))-12:parseInt(response.data.endTimeInMinutes.slice(0,2)),
                        minute:response.data.endTimeInMinutes.slice(3,5),
                        ampm:parseInt(response.data.endTimeInMinutes.slice(0,2))>12?'PM':'AM'
                    },
                    loading:false
            }))
        })
    }
    async submitAccountDetailForm(event, token) {
        var ST=this.state.startTime.ampm==='AM'?
        this.state.startTime.hour+':'+this.state.startTime.minute+':00'
        : (this.state.startTime.hour+12)+':'+this.state.startTime.minute+':00';
        var ET=this.state.endTime.ampm==='AM'?
        this.state.endTime.hour+':'+this.state.endTime.minute+':00'
        : (this.state.endTime.hour+12)+':'+this.state.endTime.minute+':00'
        await this.setState({ 
            accountDetail: { ...this.state.accountDetail, 
                startTimeInMinutes: ST,endTimeInMinutes: ET
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
            axios.put('/api/AccountDetails/' + this.state.id, form_data, config)
        }
        catch (res) {
            console.log(res)
        }
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
    getTimeHourDropdown(){
        let timeHourDropdown=[]
        for(let i=1;i<=12;i++){
            timeHourDropdown.push({
                key:i,
                text:i,
                value:i
            })
        }
        return timeHourDropdown;
    }
    render(){
        return(
            this.state.loading?
            null:
            <Segment padded="very">
            <AuthContext.Consumer>
                {({ isAuth, token }) => (
                    <div>
                        <Header as='h3'>Update Account Detail</Header>
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
                                onChange={(e,{value})=>this.setState({startTime:{...this.state.startTime,hour:value}})}
                                />
                                <Dropdown
                                compact
                                options={this.state.minuteDropdown}
                                selection
                                value={this.state.startTime.minute}
                                onChange={(e,{value})=>this.setState({startTime:{...this.state.startTime,minute:value}})}
                                />
                                <Dropdown
                                compact
                                options={this.state.ampmDropdown}
                                selection
                                value={this.state.startTime.ampm}
                                onChange={(e,{value})=>this.setState({startTime:{...this.state.startTime,ampm:value}})}
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
                                onChange={(e,{value})=>this.setState({endTime:{...this.state.endTime,hour:value}})}
                                />
                                <Dropdown
                                compact
                                options={this.state.minuteDropdown}
                                selection
                                value={this.state.endTime.minute}
                                onChange={(e,{value})=>this.setState({endTime:{...this.state.endTime,minute:value}})}
                                />
                                <Dropdown
                                compact
                                options={this.state.ampmDropdown}
                                selection
                                value={this.state.endTime.ampm}
                                onChange={(e,{value})=>this.setState({endTime:{...this.state.endTime,ampm:value}})}
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
                                    // min={this.state.nowDate}
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
                                    value={this.state.accountDetail.effectiveEndDate}
                                    min={this.state.accountDetail.effectiveStartDate} />
                            </Form.Field>
                            <Form.Field inline>
                                <label>Visibility</label>
                                <Checkbox
                                    onChange={(e, { value }) => this.setState({ accountDetail: { ...this.state.accountDetail, isVisible: !this.state.accountDetail.isVisible } })}
                                    toggle
                                    checked={this.state.accountDetail.isVisible}
                                />
                            </Form.Field>
                            <Button positive floated="right"
                                onClick={(event) => this.submitAccountDetailForm(event, token)}
                            >Update</Button>
                            <Button type='clear' floated="right"
                            onClick={(e)=>this.props.history.goBack()}
                            >Cancel</Button>
                        </Form>
                    </div>
                )}
            </AuthContext.Consumer>
        </Segment>
        )
    }
}
