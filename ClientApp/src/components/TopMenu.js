import React, { Component } from 'react';
import AuthContext from '../AuthContext';
import { Menu, Icon } from 'semantic-ui-react'

export class TopMenu extends Component {
    displayName = TopMenu.name;
    state = {}

    handleItemClick = (e, { name }) => this.setState({ activeItem: name })

    render() {

        return (
            <header>
                <AuthContext.Consumer>
                    {({ isAuth, token, role, fullname, logout }) => (

                        isAuth ?
                            <Menu secondary floated='right' fluid>
                                <Menu.Item header position='right'>Hello, {fullname}!</Menu.Item>
                                {/* {role === 'Admin' ?
                                    <Menu.Item
                                    // onClick={() => { logout() }}
                                    >
                                        <Icon name='user' />
                                        Manage Instructor Account
								</Menu.Item>
                                    :
                                    null
                                } */}
                                <Menu.Item onClick={() => { logout() }}>
                                    <Icon name='sign-out' />
                                    Logout
								</Menu.Item>
                            </Menu>
                            :
                            null
                    )
                    }
                </AuthContext.Consumer >
            </header >
        )
    }
}
