import React, { Component } from 'react'
import { Form, Button, Grid, Input, Header, Divider, Message } from 'semantic-ui-react';
import axios from 'axios';
import AuthContext from '../AuthContext';


export class SignUp extends Component {
  displayName = SignUp.name
  constructor(props) {
    super(props);
    this.state = {
      email: '',
      username: '',
      fullname: '',
      password: '',
      password2: '',
      errorMsg:'',
      error:false
    };
  }

  validateForm() {
    return this.state.email.length > 0
      && this.state.username.length > 0
      && this.state.fullname.length > 0
      && this.state.password.length > 0
      && this.state.password2.length > 0
  }
  handleChange = (e, { name, value }) => {
    this.setState({
      [name]: value
    })
  }
  handleLogin = (event)=>{
    // if (!validator.isEmail(state.email.value)) {
    //   state.email.isValid = false;
    //   state.errorMsg = 'Not a valid email address';
    //   this.setState(state);
    //   return false;
    // }
    if (this.state.password!==this.state.password2) {
      console.log("got run leh")
      this.setState({
        error : true,
        errorMsg : 'Password does not match'
      })
      return;
    }  
    
    const config = {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    }
    var bodyFormData = new FormData();
    bodyFormData.set('loginUserName',this.state.username);
    bodyFormData.set('fullName',this.state.fullname);
    bodyFormData.set('email',this.state.email);
    bodyFormData.set('password',this.state.password);
    
    axios.post('users/signup',
         bodyFormData, config        
    )
    .then(res => {
      console.log(res);
      if(res.data.signUpStatus===true){
        
        //this.displayFormMessage('You have created the account. Please login.')
      }
    }).catch((error)=>{
    if (error.response) {
      //todo
        
    } else if (error.request) {
        console.log(error.request);
    } else {
        // Something happened in setting up the request that triggered an Error
        
    }
    console.log(error.config);
    })

}
  render() {
    return (
      <Grid centered padded='vertically'>
        <Grid.Column>
          <Header as='h2' textAlign='center'>Sign Up</Header>
          <Divider />
          <Message negative hidden={!this.state.error}>
            <p>{this.state.errorMsg}</p>
          </Message>
              <Form>
                <Form.Field >
                  <label>Email</label>
                  <Input
                    placeholder='Email'
                    icon='mail'
                    iconPosition='left'
                    name="email"
                    value={this.state.email}
                    onChange={this.handleChange} />
                </Form.Field>
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
                <Form.Field >
                  <label>Full Name</label>
                  <Input
                    placeholder='Full Name'
                    icon='user'
                    iconPosition='left'
                    name="fullname"
                    value={this.state.fullname}
                    onChange={this.handleChange} />
                </Form.Field>
                <Form.Field error={this.state.error}>
                  <label>Password</label>
                  <Input
                    placeholder='Password'
                    icon='lock'
                    iconPosition='left'
                    name="password"
                    value={this.state.password.value}
                    onChange={this.handleChange}
                    type="password"
                  />
                </Form.Field>
                <Form.Field error={this.state.error}>
                  <label>Confirm Password</label>
                  <Input
                    placeholder='Confirm Password'
                    icon='lock'
                    iconPosition='left'
                    name="password2"
                    value={this.state.password2.value}
                    onChange={this.handleChange}
                    type="password" />
                </Form.Field>
                <Button
                  type='submit'
                  fluid
                  positive
                  disabled={!this.validateForm()}
                  onClick={(event) => this.handleLogin(event)}
                >Login</Button>
              </Form>
          <Divider />
          <div style={{ fontSize: 12 + 'px', textAlign: 'center' }}>
            Already an account? <a href="./Login">Login</a>
          </div>
        </Grid.Column>
      </Grid>
    )
  }
}
