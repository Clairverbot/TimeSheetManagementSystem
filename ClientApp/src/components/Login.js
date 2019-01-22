import React, { Component } from 'react';
import { Form, Button, Grid, Input, Header, Divider, GridColumn } from 'semantic-ui-react';
import axios from 'axios';
import AuthContext from '../AuthContext';

export class Login extends Component {
	displayName = Login.name
	constructor(props) {
		super(props);
		this.state = {
			username: '',
			password: ''
		};
	}

	validateForm() {
		return this.state.username.length > 0 && this.state.password.length > 0;
	}
	handleChange = (e, { name, value }) => {
		this.setState({
			[name]: value
		})
	}
	handleLogin = (event, callback) => {
		const config = {
			headers: {
				'Content-Type': 'multipart/form-data'
			}
		}
		//Preparing a key-value pair form-data to submit to server side api method
		//Reference: https://stackoverflow.com/questions/47630163/axios-post-request-to-send-form-data
		//Took me 4 hours on 9 Dec 2018 to figure this out. I kept on having 415 unsupported media type or
		//empty values inside the inFormData input parameter variable at the Asp.Net core side.
		var bodyFormData = new FormData();
		bodyFormData.set('username', this.state.username);
		bodyFormData.set('password', this.state.password);



		axios.post('/users/signin', bodyFormData, config)
			.then(res => {
				callback(true, res.data.token,res.data.user.role)
				this.props.history.push('/')
			}).catch((error) => {
				// Error
				if (error.response) {
					console.log(error.response.data.message)

				} else if (error.request) {
					console.log(error.request);
				} else {
					console.log('Error', error.message);
				}
				console.log(error.config);
			})

	}
	render() {
		return (
			<div
			style={{ display: 'flex', justifyContent:'center', padding:2+'%',height:100+'%',background:'linear-gradient(to right, #4ecdc4, #556270)'}}
			>
			<Grid 
			centered 
			padded='vertically'
			style={{paddingTop:218.5+'px',paddingBottom:218.5+'px',}}>
				<Grid.Column style={{background:'#ffffffee'}}>
					<Header as='h2' textAlign='center'>Welcome</Header>
					<AuthContext.Consumer>
						{({ updateAuth }) => (
							<Form>
								<Form.Field >
									<label>Username</label>
									<Input
										placeholder='Username'
										icon='user'
										iconPosition='left'
										name="username"
										value={this.state.username}
										onChange={this.handleChange} />
								</Form.Field>
								<Form.Field>
									<label>Password</label>
									<Input
										placeholder='Password'
										icon='lock'
										iconPosition='left'
										name="password"
										value={this.state.password}
										onChange={this.handleChange}
										type="password" />
								</Form.Field>
								<Button
									type='submit'
									fluid
									color='teal'
									disabled={!this.validateForm()}
									onClick={(event) => this.handleLogin(event, updateAuth)}
								>Login</Button>
							</Form>

						)}
					</AuthContext.Consumer>
					{/* <Divider />
					<div style={{ fontSize: 12 + 'px', textAlign: 'center' }}>
						Don't have an account? <a href="./SignUp">Sign Up</a>
					</div> */}
				</Grid.Column>
			</Grid>
			</div>
		)
	}
}
