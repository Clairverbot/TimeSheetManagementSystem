import React, { Component } from 'react';
import AuthContext from './../../AuthContext';
import { Segment, Header, Form, Divider, Label } from 'semantic-ui-react';
import axios from 'axios';

export class CreateAccountDetail extends Component {
    displayName = CreateAccountDetail.name
    render() {
        return (
            <Segment padded="very">
                <AuthContext.Consumer>
                    {({ isAuth, token }) => (
                        <div>
                            <Header as='h3'>Create Account Detail</Header>
                            <Form>
                            </Form>
                        </div>
                    )}
                </AuthContext.Consumer>
            </Segment>
        )
    }
}