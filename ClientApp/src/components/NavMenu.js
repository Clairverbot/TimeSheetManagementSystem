import React, { Component } from 'react';
import AuthContext from '../AuthContext';
import { Icon, Menu, Header, Divider } from 'semantic-ui-react'

export class NavMenu extends Component {
	displayName = NavMenu.name;
	state = {}

	handleItemClick = (e, { name }) => this.setState({ activeItem: name })

	render() {

		return (
			<header>
				<AuthContext.Consumer>
					{({ isAuth, token, role, logout }) => (
						console.log(role),
						<Menu secondary vertical color='teal' inverted borderless fixed='left'>
							<Menu.Item href="/" position="left">
								<Header as='h4' inverted>Time Sheet Management System</Header>
							</Menu.Item>
							{isAuth ? (

								[
									<Divider />,
									<Menu.Item>
										<Menu.Header>Session Synopsis</Menu.Header>
										<Menu.Menu>
											<Menu.Item href="./SessionSynopsis/Manage">Manage Session Synopsis</Menu.Item>
											<Menu.Item href="./SessionSynopsis/Create">Create Session Synopsis</Menu.Item>
										</Menu.Menu>
									</Menu.Item>,
									role === 'Admin' ?
										[
											<Divider />,
											<Menu.Item>
												<Menu.Header>Customer Account</Menu.Header>
												<Menu.Menu>
													<Menu.Item href="./CustomerAccount/Manage">Manage Customer Account</Menu.Item>
													<Menu.Item href="./CustomerAccount/Create">Create Customer Account</Menu.Item>
												</Menu.Menu>
											</Menu.Item>,
											<Divider />,
											<Menu.Item>
												<Menu.Header>Account Detail</Menu.Header>
												<Menu.Menu>
													<Menu.Item href='./AccountDetail/Manage'>Manage Account Detail</Menu.Item>
													<Menu.Item href='./AccountDetail/Create'>Create Account Detail</Menu.Item>
												</Menu.Menu>
											</Menu.Item>
										]
										:
										// console.log(user)
										null
									,
									<Divider />,
									<Menu.Item key={2} onClick={() => { logout() }}>
										<Icon name='sign-out' />
										Logout
								</Menu.Item>,
								]
							) :

								[
									<Menu.Item key={3} href="./login">
										Login
          				</Menu.Item>,
									// 				<Menu.Item key={4} href="./signup">
									// 					Sign Up
									// </Menu.Item>
								]
							}
						</Menu>
					)}
				</AuthContext.Consumer>
			</header>
		)
	}
}
