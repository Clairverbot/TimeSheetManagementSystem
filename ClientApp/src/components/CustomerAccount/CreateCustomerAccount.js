import React, { Component } from 'react';
import AuthContext from './../../AuthContext';
import { Segment, Header, Form, Divider, Checkbox, Button, Label } from 'semantic-ui-react';
import axios from 'axios';

export class CreateCustomerAccount extends Component {
	displayName = CreateCustomerAccount.name

	constructor(props) {
		super(props)
		var d = new Date();
		this.state = {
			accountName: '',
			comments: '',
			visibility: true,
			rate: '',
			nowDate: d.getFullYear() + "-" + (d.getMonth() + 1),
			startDate: d.getFullYear() + "-" + ('0' + d.getMonth() + 1).slice(-2),
			endDate: '',
			showError: false,
			errorMsg: ''
		}
		this.submitCustomerAccountForm = this.submitCustomerAccountForm.bind(this);
	}
	submitCustomerAccountForm(event, token) {
		if (this.state.accountName === '') {
			this.setState({
				showError: true,
			})
			return
		}
		var config = {
			withCredentials: false,
			headers: {
				'Content-Type': 'multipart/form-data',
				'Authorization': 'Bearer ' + token
			}
		}
		let body = new FormData();
		body.set('accountName', this.state.accountName)
		body.set('isVisible', this.state.visibility)
		body.set('comments', this.state.comments)
		body.set('ratePerHour', this.state.rate)
		body.set('effectiveStartDate', this.state.startDate + "-01")
		body.set('effectiveEndDate', this.state.endDate + "-01")

		console.log(body)
		axios.post('/api/customerAccounts', body, config)
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
							<Header as='h3'>Create Customer Account</Header>
							<Form>
								<Form.Field inline>
									<Form.Input
										required={true}
										fluid
										onChange={(e) => this.setState({ accountName: e.target.value })}
										label='Account Name'
										// pattern="^[A-Za-z] .+$"
										// title="Account Name contains weird stuff"
										placeholder='eg. Wakanda'
										value={this.state.accountName} />
								</Form.Field>
								<Form.Field inline>
									<Form.TextArea required={false}
										onChange={(e) => this.setState({ comments: e.target.value })}
										label='Comments'
										placeholder='Some comments here..'
										value={this.state.comments} />
								</Form.Field>
								<Form.Field inline>
									<label>Visibility</label>
									<Checkbox
										onChange={(e) => this.setState({ visibility: !this.state.visibility })}
										toggle
										checked={this.state.visibility}
									/>
								</Form.Field>
								<Divider horizontal>
									<Header as='h5'>
										Account Rate
								</Header>
								</Divider>
								<Form.Field inline>
									<Form.Input
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
										type='month'
										onChange={(e) => this.setState({ startDate: e.target.value })}
										label='Effective Start Date'
										value={this.state.startDate}
										min={this.state.nowDate} />
								</Form.Field>
								<Form.Field inline>
									<Form.Input
										fluid
										type='month'
										onChange={(e) => this.setState({ endDate: e.target.value })}
										label='Effective End Date'
										value={this.state.endDate}
										min={this.state.startDate} />
								</Form.Field>
								<Button positive floated="right" onClick={(event) => this.submitCustomerAccountForm(event, token)}>Create</Button>
								<Button type='clear' floated="right" onClick={(e) => this.props.history.goBack()}>Cancel</Button>
							</Form>
						</div>
					)}
				</AuthContext.Consumer>
			</Segment>
		)
	}
}
