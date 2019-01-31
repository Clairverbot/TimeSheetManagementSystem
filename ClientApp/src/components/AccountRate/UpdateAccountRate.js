import React, { Component } from 'react'
import AuthContext from './../../AuthContext';
import { Segment,Message, Header, Form, Button, Label } from 'semantic-ui-react';
import axios from 'axios';
import moment from 'moment'

export class UpdateAccountRate extends Component {
    displayName = UpdateAccountRate.name
    constructor(props) {
        super(props)
        var d = new Date();
        this.state = {
            id:props.selectedTaskIdForUpdate,
            rate: '',
            nowDate: d.getFullYear() + "-" + (d.getMonth() + 1),
            startDate: '',
            endDate: '',
			existingRate: [],
			showError: false,
			errorMsg: ''
        }
        fetch('/api/AccountRates/' + this.state.id, {
            withCredentials: false,
            headers: {
                'Authorization': 'Bearer ' + this.props.token
            },
        })
            .then(response => response.json())
            .then(data => {
                this.setState({
                    rate: data.ratePerHour,
                    startDate: data.effectiveStartDate.slice(0, -12),
                    endDate: data.effectiveEndDate.slice(0, -12)
                });
                console.log(data)
            });
            fetch('/api/AccountRates/GetDatesByCustId/' + this.state.id, {
                withCredentials: false,
                headers: {
                    'Authorization': 'Bearer ' + this.props.token
                },
            })
                .then(response => response.json())
                .then(data => { console.log(data); this.setState({ existingRate: data }) })
        for(let i=0;i<this.state.existingRate.length;i++){
            if(this.state.existingRate[i].effectiveStartDate===this.state.startDate.replace('-', '')){
                this.state.existingRate.splice(i)
            }
        }
        this.submitAccountRateForm = this.submitAccountRateForm.bind(this);
    }
    submitAccountRateForm(event, token) {
		this.setState({
			showError:false,
			errorMsg:''
		})
		if(this.state.rate===''){
			this.setState({
				showError: true,
				errorMsg: 'Enter Account Rate la'
			})
			return
		}
		if(this.state.startDate===''){
			this.setState({
				showError: true,
				errorMsg: 'Select Start Date la'
			})
			return
        }
        let d1 = this.state.startDate.replace('-', '')
        let d2 = this.state.endDate.replace('-', '')
        if(this.state.existingRate.length!=1){
		this.state.existingRate.forEach(rate => {
			if ((d1 >= rate.effectiveStartDate &&
				d1 <= rate.effectiveEndDate) || (
					d2 <= rate.effectiveStartDate &&
					d2 <= rate.effectiveEndDate)) {
				this.setState({
					showError: true,
					errorMsg: 'Existing Rate $' + rate.ratePerHour + ' from ' + rate.effectiveStartDate + ' to ' + rate.effectiveEndDate
				})
				throw "exit"
			}
        });
    }
        var config = {
            withCredentials: false,
            headers: {
                'Content-Type': 'multipart/form-data',
                'Authorization': 'Bearer ' + token
            }
        }
        let body = new FormData();
        body.set('ratePerHour', this.state.rate)
        body.set('effectiveStartDate', this.state.startDate + "-01")
        body.set('effectiveEndDate', this.state.endDate+ '-' + moment(this.state.endDate + '-01').endOf('month').format('DD'))

        axios.put('/api/accountRates/' + this.state.id, body, config)
            .then(res => {
                alert(res.data.message);
                if (res.status === 200) {
                    window.location.href = "./CustomerAccount/Manage"
                }
            });
    }
    render() {
        return (
            <Segment padded="very">
                <AuthContext.Consumer>
                    {({ isAuth, token }) => (
                        <div>
                            <Header as='h3'>Update Account Rate</Header>
                            <Form>
								<Message hidden={!this.state.showError} compact negative>
									<Message.Header>
										There was some errors with your submission
									</Message.Header>
									<p>{this.state.errorMsg}</p>
								</Message>
                                <Form.Field inline>
                                    <Form.Input required={true}
                                        onChange={(e) => this.setState({ rate: e.target.value })}
                                        label='Rate per Hour'
                                        placeholder='Amount'
                                        value={this.state.rate}
                                        labelPosition='right'
                                        type='number'>
                                        <Label basic>$</Label>
                                        <input />
                                        <Label>/hr</Label>
                                    </Form.Input>
                                </Form.Field>
                                <Form.Field inline>
                                    <Form.Input
                                        fluid
                                        required={true}
                                        type='month'
                                        onChange={(e) => this.setState({ startDate: e.target.value })}
                                        label='Effective Start Date'
                                        value={this.state.startDate}
                                        min={this.state.nowDate} />
                                </Form.Field>
                                <Form.Field inline>
                                    <Form.Input
                                        fluid
                                        required={true}
                                        type='month'
                                        onChange={(e) => this.setState({ endDate: e.target.value })}
                                        label='Effective End Date'
                                        value={this.state.endDate}
                                        min={this.state.startDate} />
                                </Form.Field>
                                <Button positive floated="right" onClick={(event) => this.submitAccountRateForm(event, token)}>Update</Button>
                                <Button type='clear' floated="right" onClick={(e)=>this.props.history.goBack()}>Cancel</Button>
                            </Form>
                        </div>
                    )}
                </AuthContext.Consumer>
            </Segment>
        )
    }
}
