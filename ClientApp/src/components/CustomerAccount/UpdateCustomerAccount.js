import React, { Component } from 'react'
import AuthContext from './../../AuthContext';
import { Segment, Header, Form, Divider, Checkbox, Button, Label } from 'semantic-ui-react';
import axios from 'axios';

export class UpdateCustomerAccount extends Component {
  displayName=UpdateCustomerAccount.name
  constructor(props) {
		super(props)
		this.state = {
      id:props.selectedTaskIdForUpdate,
      accountName:'',
			comments: '',
      visibility: true,
      showError:false
    }
    fetch('/api/CustomerAccounts/' + this.state.id,{
			withCredentials: false,
            headers: {
                'Authorization': 'Bearer ' + this.props.token
            },
		})
			.then(response => response.json())
			.then(data => {
				this.setState({
					accountName: data.customerAccountName,
          visibility: data.isVisible,
          comments:data.comments
				});
			});
      this.submitCustomerAccountForm = this.submitCustomerAccountForm.bind(this);
    }
    submitCustomerAccountForm() {
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
        'Authorization': 'Bearer ' + this.props.token } 
      }
      let bodyFormData = new FormData();
      bodyFormData.set('accountName',this.state.accountName);
      bodyFormData.set('isVisible', this.state.visibility);
      bodyFormData.set('comment',this.state.comments);
      axios.put(`/api/customerAccounts/${this.state.id}`,  bodyFormData, config )
      .then(res=>{
        if(res.status===200){
          console.log(res)
          alert(res.data.message)
          this.props.history.push("../Manage");
        }
        else if(res.status===400){
          alert("Customer Account Name already exist!")
        }
      })
    };
  render() {
    return (
      <Segment padded="very">
        <AuthContext.Consumer>
          {({ isAuth, token }) => (
            <div>
              <Header as='h3'>Update Customer Account</Header>
              <Form>
                <Form.Field inline>
                  <Form.Input
                    required={true}
                    fluid
                    error={this.state.showError}
                    onChange={(e) => this.setState({ accountName: e.target.value })}
                    label='Account Name'
                    pattern="[a-zA-Z][^#&<>~;$^%{}?]{1,20}$"
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
                <Button positive floated="right" onClick={(event) => this.submitCustomerAccountForm(event, token)}>Update</Button>
                <Button type='clear' floated="right" onClick={(e)=>this.props.history.goBack()}>Cancel</Button>
              </Form>
            </div>
          )}
        </AuthContext.Consumer>
      </Segment>
    )
  }
}
