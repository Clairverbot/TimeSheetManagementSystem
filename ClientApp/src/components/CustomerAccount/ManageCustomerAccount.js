import React, { Component } from 'react';
import { Icon, Pagination, Table, Header, Button, Grid, Search, Loader, Confirm, Dropdown, Menu } from 'semantic-ui-react';
import AuthContext from './../../AuthContext';
import axios from 'axios';
import _ from 'lodash'
export class ManageCustomerAccount extends Component {
	displayName = ManageCustomerAccount.name

	constructor(props) {
		super(props);
		this.state = {
			customerAccounts: [],
			accountRates: [],
			loading: true,
			selectRow:false,
			//pagination
			pages: 0,
			//search bar
			value: "",
			results: [],
			filteredResults: [],
			//pagination
			activePage: 1,
			totalPages: 0,
			//sort
			column: null,
			direction: null,
			column1: null,
			direction1: null,
			//delete
			showDeleteDialog: false,
			showDeleteDialog2: false,
			selectedAccount: {},
			selectedRateId: 0,
			//show account rate
			showAccountRate: true
		};
		this.deleteDialog = this.deleteDialog.bind(this);

		var config = {
			withCredentials: false,
			headers: {
				'Authorization': 'Bearer ' + this.props.token
			}
		}

		axios.get('/api/CustomerAccounts', config)
			.then(response => {
				this.setState({
					customerAccounts: response.data,
					loading: false,
					totalPages: response.data.length / 5
				});
				this.state.customerAccounts.forEach(element => {
					if (element.accountRates.length != 0) {
						this.state.accountRates.push(element.accountRates)
					}
				});
			}
			);
		this.populateSearchResults(this.state.customerAccounts);
	};

	// #region search
	populateSearchResults(customerAccounts) {
		customerAccounts.forEach(x => {
			this.state.results.push({
				title: x.accountName,
				description: "Comments: " + x.comments + "\nCreated By: " + x.createdBy + "\nUpdated By: " + x.updatedBy
			})
		});
	}
	handleSearchChange = (e, { value }) => {
		value = value.toLowerCase();
		this.state.customerAccounts.forEach(x => {
			if (x.accountName.toLowerCase().includes(value) || x.createdBy.toLowerCase().includes(value) || x.updatedBy.toLowerCase().includes(value)) {
				this.state.filteredResults.push({
					title: x.accountName,
					description: "Comments: " + x.comments + "\nCreated By: " + x.createdBy + "\n Updated By: " + x.updatedBy
				});
			}
		});
		this.setState(state => ({
			results: this.state.filteredResults,
			filteredResults: []
		}));
	}
	handleResultSelect() {

	}
	// #endregion

	//#region sort
	handleSort = clickedColumn => () => {
		const { column, customerAccounts, direction } = this.state

		if (column !== clickedColumn) {
			this.setState({
				column: clickedColumn,
				customerAccounts: _.sortBy(customerAccounts, [clickedColumn]),
				direction: 'ascending',
			})

			return
		}

		this.setState({
			customerAccounts: customerAccounts.reverse(),
			direction: direction === 'ascending' ? 'descending' : 'ascending',
		})
	}
	//todo
	handleSortForAccRate = clickedColumn => () => {
		const { column1, customerAccounts,accountRates, direction1 } = this.state

		if (column1 !== clickedColumn) {
			this.setState({
				column1: clickedColumn,
				customerAccounts: _.sortBy(customerAccounts, ['customerAccountId', accountRates.clickedColumn]),
				direction1: 'ascending',
			})

			return
		}

		this.setState({
			accountRates: accountRates.reverse(),
			direction1: direction1 === 'ascending' ? 'descending' : 'ascending',
		})
	}



	//#endregion

	//#region customer account
	// #region delete customer account
	deleteDialog(customerAccount, e) {
		this.setState(state => ({
			selectedAccount: customerAccount,
			showDeleteDialog: !state.showDeleteDialog,
		}));

	}

	deleteCustomerAccount(id, e) {
		fetch('/api/CustomerAccounts/' + id, {
			method: 'DELETE',
			withCredentials: false,
			headers: {
				'Authorization': 'Bearer ' + this.props.token
			},
		}
		)
			.then(window.location.href = "./CustomerAccount/Manage")
		this.setState(state => ({
			showDeleteDialog: false
		}));

	}
	//#endregion

	//#region update customer account
	handleUpdateClick(e, id, callback) {
		console.log(id)
		callback(id)
		this.props.history.push("./Update");
	}
	//#endregion
	//#endregion

	//#region account rate
	//#region add
	handleAddRateClick(e, id, callback) {
		callback(id)
		this.props.history.push("./AccountRate/Create")
	}
	//#endregion
	//#region add
	handleUpdateRateClick(e, id, callback) {
		callback(id)
		this.props.history.push("./AccountRate/Update")
	}
	//#endregion
	//#region delete
	deleteAccountRate(id, e) {
		//ToDO
		fetch('/api/AccountRates/' + id, {
			method: 'DELETE',
			withCredentials: false,
			headers: {
				'Authorization': 'Bearer ' + this.props.token
			},
		}
		)
			.then(window.location.href = "./CustomerAccount/Manage")
		this.setState(state => ({
			showDeleteDialog: false
		}));
	}
	//#endregion
	//#endregion
	renderSessionSynopsesTable(customerAccounts) {
		//let contents = this.renderAccountRates(customerAccounts.accountRates)
		return (
			<AuthContext.Consumer>
				{({ selectTask }) => (
					customerAccounts.length === 0
						? <Table.Body>
							<Table.Row>
								<Table.Cell colSpan='13'>
									<Grid centered rows='2'>
										<br />
										<Grid.Row centered>
											<Button circular icon='plus' positive size="large" href="./CustomerAccount/Create" />
										</Grid.Row>
										<Grid.Row centered>
											<Header icon as='h4' textAlign='center'>
												Add Your First Customer Account
                	</Header>
										</Grid.Row>
										<br />
									</Grid>
								</Table.Cell>
							</Table.Row>
						</Table.Body>
						:
						customerAccounts.map((customerAccount, index) => {
							return Math.ceil((index + 1) / 5) === parseInt(this.state.activePage) ?
								<Table.Body>
									<Table.Row
										key={customerAccount.customerAccountId}
										warning={!customerAccount.isVisible}
										verticalAlign='top'
									>
										<Table.Cell rowSpan={customerAccount.accountRates.length + 1}>{index+1}</Table.Cell>
										<Table.Cell rowSpan={customerAccount.accountRates.length + 1}>{customerAccount.accountName}</Table.Cell>
										<Table.Cell hidden={this.state.showAccountRate} rowSpan={customerAccount.accountRates.length + 1}>{customerAccount.comments}</Table.Cell>
										<Table.Cell rowSpan={customerAccount.accountRates.length + 1}>{customerAccount.isVisible
											? <Icon name="check" />
											: <Icon name="close" />}
										</Table.Cell>
										<Table.Cell rowSpan={customerAccount.accountRates.length + 1}>{customerAccount.createdBy}</Table.Cell>
										<Table.Cell hidden={this.state.showAccountRate} rowSpan={customerAccount.accountRates.length + 1}>{customerAccount.updatedBy}</Table.Cell>
										<Table.Cell rowSpan={customerAccount.accountRates.length + 1}>
											<Button.Group size="mini">
												<Button icon='edit'
													positive
													onClick={(event) => this.handleUpdateClick(event, customerAccount.customerAccountId, selectTask)} />
												<Button
													negative
													icon='trash'
													onClick={this.deleteDialog.bind(this, customerAccount)}
												/>
											</Button.Group>
										</Table.Cell>
										{customerAccount.accountRates.length !== 0 ?
											([
												<Table.Cell key={0} hidden={!this.state.showAccountRate}>$ {customerAccount.accountRates[0].ratePerHour}</Table.Cell>,
												<Table.Cell key={1} hidden={!this.state.showAccountRate}>{customerAccount.accountRates[0].effectiveStartDate.slice(0, -12)}</Table.Cell>,
												<Table.Cell key={2} hidden={!this.state.showAccountRate}>{customerAccount.accountRates[0].effectiveEndDate.slice(0, -12)}</Table.Cell>,
												<Table.Cell key={3} hidden={!this.state.showAccountRate}>
													<Button.Group size="mini">
														<Button
															icon='edit' 
															positive
															onClick={(event) => this.handleUpdateRateClick(event, customerAccount.accountRates[0].accountRateId, selectTask)}
														/>
														<Button
															icon='trash' 
															negative
															onClick={(e) => this.setState({
																showDeleteDialog2: !this.state.showDeleteDialog2,
																selectedRateId: customerAccount.accountRates[0].accountRateId
															})}
															disabled={customerAccount.accountRates.length <= 1}
														/>
													</Button.Group>
												</Table.Cell>,
												<Table.Cell key={4} rowSpan={customerAccount.accountRates.length + 1} verticalAlign='middle' hidden={!this.state.showAccountRate}>
													<Button
														circular
														icon='add'
														basic
														size='tiny'
														onClick={(e) => this.handleAddRateClick(e, customerAccount.customerAccountId, selectTask)} />
												</Table.Cell>
											])
											:
											[
												<Table.Cell key={5} colSpan={5} hidden={!this.state.showAccountRate} textAlign='center'>
													<Button
														circular
														icon='add'
														positive
														size='tiny'
														onClick={(e) => this.handleAddRateClick(e, customerAccount.customerAccountId, selectTask)} />
												</Table.Cell>
											]
										}
									</Table.Row>
									{customerAccount.accountRates.map((item, index) => {
										return (
											index !== 0 ?
												<Table.Row warning={!customerAccount.isVisible} hidden={!this.state.showAccountRate}>
													<Table.Cell>$ {item.ratePerHour}</Table.Cell>
													<Table.Cell>{item.effectiveStartDate.slice(0, -12)}</Table.Cell>
													<Table.Cell>{item.effectiveEndDate.slice(0, -12)}</Table.Cell>
													<Table.Cell>
														<Button.Group size="mini">
															<Button 
															icon='edit'
															positive
																onClick={(event) => this.handleUpdateRateClick(event, item.accountRateId, selectTask)}
															/>
															<Button 
															icon='trash'
															negative
																onClick={(e) => this.setState({
																	showDeleteDialog2: !this.state.showDeleteDialog2,
																	selectedRateId: item.accountRateId
																})}
																disabled={customerAccount.accountRates.length <= 1}
															/>
														</Button.Group>
													</Table.Cell>
												</Table.Row>
												:
												<Table.Row>

												</Table.Row>
										)
									}

									)}
								</Table.Body>
								:
								null
						}
						)

				)
				}
			</AuthContext.Consumer>
		);
	}
	render() {
		let contents = this.state.loading
			? <Table.Body>
				<Table.Row>
					<Table.Cell colSpan='13'>
						<Loader active inline='centered' />
					</Table.Cell>
				</Table.Row>
			</Table.Body>
			: this.renderSessionSynopsesTable(this.state.customerAccounts);

		return (
			<div>
				<Header as='h2'>
					Manage Customer Account
                </Header>
				<Table sortable celled selectable structured size='small' >
					<Table.Header>
						<Table.Row>
							<Table.HeaderCell colSpan='13'>
								<Grid columns='equal'>
									<Grid.Row>
										<Grid.Column>
											<Search size="mini"
												results={this.state.results}
												onSearchChange={this.handleSearchChange}
												onResultSelect={this.handleResultSelect} />
										</Grid.Column>
										<Grid.Column>
											<Button icon labelPosition='left' floated='right' positive size="mini" href="./CustomerAccount/Create">
												<Icon name='plus' />
												Add Customer Account
                            </Button>
										</Grid.Column>
									</Grid.Row>
								</Grid>
							</Table.HeaderCell>
						</Table.Row>
						<Table.Row>
							<Table.HeaderCell
								rowSpan={2}>#</Table.HeaderCell>
							<Table.HeaderCell rowSpan={2}
								sorted={this.state.column === 'accountName' ? this.state.direction : null}
								onClick={this.handleSort('accountName')}
							>Account Name</Table.HeaderCell>
							<Table.HeaderCell 
							rowSpan={2}
							hidden={this.state.showAccountRate}>Comments</Table.HeaderCell>
							<Table.HeaderCell rowSpan={2}
								sorted={this.state.column === 'isVisible' ? this.state.direction : null}
								onClick={this.handleSort('isVisible')}
							>Visibility</Table.HeaderCell>
							<Table.HeaderCell rowSpan={2}
								sorted={this.state.column === 'createdBy' ? this.state.direction : null}
								onClick={this.handleSort('createdBy')}
							>Created by</Table.HeaderCell>
							<Table.HeaderCell rowSpan={2}
								sorted={this.state.column === 'updatedBy' ? this.state.direction : null}
								onClick={this.handleSort('updatedBy')}
								hidden={this.state.showAccountRate} >Updated by</Table.HeaderCell>
							<Table.HeaderCell rowSpan={2}>Actions</Table.HeaderCell>
							<Table.HeaderCell colSpan={5} hidden={!this.state.showAccountRate}>
								Account Rates
								{/* <Button floated='right' size="tiny" basic circular>
									<Dropdown icon='filter' direction='right'>
										<Menu.Menu position='right'>
											<Dropdown.Item icon='date' text='Customer where current Account Rate exists' />
										</Menu.Menu>
									</Dropdown>
								</Button> */}
							</Table.HeaderCell>
						</Table.Row>
						<Table.Row hidden={!this.state.showAccountRate}>
							<Table.HeaderCell
								sorted={this.state.column1 === 'ratePerHour' ? this.state.direction1 : null}
								onClick={this.handleSortForAccRate('ratePerHour')}>
								Rate per Hour
							</Table.HeaderCell>
							<Table.HeaderCell>Effective Start Date</Table.HeaderCell>
							<Table.HeaderCell>Effective End Date</Table.HeaderCell>
							<Table.HeaderCell>Actions</Table.HeaderCell>
							<Table.HeaderCell>Add</Table.HeaderCell>
						</Table.Row>
					</Table.Header>
					{contents}

					<Table.Footer>
						<Table.Row>
							<Table.HeaderCell colSpan='13'>
								<Button
									icon
									labelPosition='left'
									floated='left'
									positive size="mini"
									onClick={(e) => this.setState({ showAccountRate: !this.state.showAccountRate })}>
									<Icon name={this.state.showAccountRate ? 'hide' : 'unhide'} />
									{this.state.showAccountRate ? 'Hide Account Rate' : 'Show Account Rate'}
								</Button>
								<Pagination 
									floated='right'
									activePage={this.state.activePage}
									onPageChange={(e, { activePage }) => this.setState({ activePage })}
									size='mini'
									totalPages={this.state.totalPages}
									ellipsisItem={null}
									firstItem={null}
									lastItem={null}
									prevItem={null}
									nextItem={null}
								/>
							</Table.HeaderCell>
						</Table.Row>
					</Table.Footer>
				</Table>
				<Confirm
					open={this.state.showDeleteDialog}
					confirmButton='Yes'
					header={'Are you sure you want to delete  ' + this.state.selectedAccount.accountName + '?'}
					content='This action cannot be undone'
					onCancel={this.deleteDialog}
					onConfirm={this.deleteCustomerAccount.bind(this, this.state.selectedAccount.customerAccountId)}
					size='mini'
				/>
				<Confirm
					open={this.state.showDeleteDialog2}
					confirmButton='Yes'
					header={'Are you sure you want to delete?'}
					content='You sure ah'
					onCancel={(e) => this.setState({ showDeleteDialog2: !this.state.showDeleteDialog2 })}
					onConfirm={this.deleteAccountRate.bind(this, this.state.selectedRateId)}
					size='mini'
				/>
			</div>

		);
	}
}

