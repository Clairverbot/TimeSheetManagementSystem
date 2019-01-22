import React, { Component } from 'react';
import { Header, Form, Checkbox, Button, Segment } from 'semantic-ui-react';
import AuthContext from './../../AuthContext';
import axios from 'axios';

export class CreateSessionSynopsis extends Component {
  displayName = CreateSessionSynopsis.name
  constructor(props) {
    super(props)

    this.state = {
      sessionSynopsisName: '',
      visibility: true,
      sessionSynopsisNameError: false,
      complete: false
    }
    this.submitSessionSynopsisForm = this.submitSessionSynopsisForm.bind(this);
    this.successCallback = this.successCallback.bind(this);
  }
  successCallback() {
    this.setState({
      complete: true
    })
  }


  submitSessionSynopsisForm(event, token) {
    console.log(this.state.visibility)
    let error = false;
    if (this.state.sessionSynopsisName === '') {
      this.setState({ sessionSynopsisNameError: true })
      error = true
    } else {
      this.setState({ sessionSynopsisNameError: false })
      error = false
    }
    if (error) {
      this.setState({ formError: true })
      return
    } else {
      this.setState({ formError: false })
    }

    console.log(JSON.stringify({
      sessionSynopsisName: this.state.sessionSynopsisName,
      isVisible: this.state.visibility
    }))
    var config = {
      withCredentials: false,
      headers: {
        'Content-Type': 'multipart/form-data',
        'Authorization': 'Bearer ' + token
      }
    }

    let bodyFormData = new FormData();
    bodyFormData.set('sessionSynopsisName', this.state.sessionSynopsisName);
    bodyFormData.set('isVisible', this.state.visibility === true ? true : false);

    axios.post('/api/sessionSynopses', bodyFormData, config)
      .then(res => {
        alert(res.data.message);

        if (res.data.ok) {
          window.location.href = "./SessionSynopsis/Manage"
        }
      });
  };
  handleDismiss = () => {
    this.setState({ visible: false })
  };
  render() {
    return (
      <Segment padded="very">
        <AuthContext.Consumer>
          {({ isAuth, token }) => (
            <div>

              <Header as='h3'>
                Create Session Synopsis
                </Header>
              <Form>
                <Form.Field inline>
                  <Form.Input required={true}
                    onChange={(e) => this.setState({ sessionSynopsisName: e.target.value })}
                    label='Session Synopsis Name'
                    placeholder='eg. Performance'
                    value={this.state.sessionSynopsisName}
                    error={this.state.sessionSynopsisNameError} />
                </Form.Field>
                <Form.Field inline>
                  <label>Visibility</label>
                  <Checkbox
                    onChange={(e) => this.setState({ visibility: !this.state.visibility })}
                    toggle
                    checked={this.state.visibility}
                     />
                </Form.Field>

                <Button positive floated="right" onClick={(event) => this.submitSessionSynopsisForm(event, token)}>Create</Button>
                <Button type='clear' floated="right">Cancel</Button>
              </Form>
            </div>
          )}
        </AuthContext.Consumer>
      </Segment>
    );
  }


}
