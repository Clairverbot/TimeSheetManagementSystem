import React, { Component } from 'react';
import { Icon, Pagination, Table, Header, Button, Grid, Search, Loader, Confirm } from 'semantic-ui-react';
import AuthContext from './../../AuthContext';
import axios from 'axios';
import _ from 'lodash'
export class ManageSessionSynopsis extends Component {
	displayName = ManageSessionSynopsis.name

	constructor(props) {
		super(props);
		this.state = {
			sessionSynopses: [],
			loading: true,
			showDeleteDialog: false,
			//pagination
			activePage: 1,
			totalPages: 0,
			//search bar
			value: "",
			results: [],
			filteredResults: [],
			filteredSessionSynopses: [],
			//sort
			column: null,
			direction: null,
			//delete
			selectedSession: {}
		};
		this.deleteDialog = this.deleteDialog.bind(this);

		var config = {
			withCredentials: false,
			headers: {
				'Authorization': 'Bearer ' + this.props.token
			}
		}

		axios.get('/api/SessionSynopses', config)
			.then(response => {
				console.log(response.data)
				this.setState({
					sessionSynopses: response.data,
					loading: false,
					totalPages: response.data.length / 5
				})
			}
			);

		this.populateSearchResults(this.state.sessionSynopses);
	};

	// #region search
	populateSearchResults(sessionSynopses) {
		sessionSynopses.forEach(x => {
			this.state.results.push({ title: x.sessionSynopsisName, description: "Created By: " + x.createdBy + "\nUpdated By: " + x.updatedBy })
		});
	}
	handleSearchChange = (e, { value }) => {
		value = value.toLowerCase();
		this.state.sessionSynopses.forEach(x => {
			if (x.sessionSynopsisName.toLowerCase().includes(value) || x.createdBy.toLowerCase().includes(value) || x.updatedBy.toLowerCase().includes(value)) {
				this.state.filteredResults.push({ title: x.sessionSynopsisName, description: "Created By: " + x.createdBy + "\n Updated By: " + x.updatedBy });
			}
		});
		this.setState(state => ({
			results: this.state.filteredResults,
			filteredResults: []
		}));
	}
	handleResultSelect(e,{results}) {

	}
	// #endregion

	//#region sort
	handleSort = clickedColumn => () => {
		const { column, sessionSynopses, direction } = this.state

		if (column !== clickedColumn) {
			this.setState({
				column: clickedColumn,
				sessionSynopses: _.sortBy(sessionSynopses, [clickedColumn]),
				direction: 'ascending',
			})

			return
		}

		this.setState({
			sessionSynopses: sessionSynopses.reverse(),
			direction: direction === 'ascending' ? 'descending' : 'ascending',
		})
	}
	//#endregion

	// #region delete
	deleteDialog(sessionSynopsis, e) {
		this.setState(state => ({
			selectedSession: sessionSynopsis,
			showDeleteDialog: !state.showDeleteDialog,
		}));

	}

	deleteSessionSynopsis(id, e) {
		fetch('/api/SessionSynopses/' + id, {
			method: 'DELETE',
			withCredentials: false,
			headers: {
				'Authorization': 'Bearer ' + this.props.token
			},
		}
		)
			.then(window.location.href = "./SessionSynopsis/Manage")
		this.setState(state => ({
			showDeleteDialog: false
		}));

	}
	//#endregion

	//#region update
	handleUpdateClick(e, id, callback) {
		console.log(id)
		callback(id)
		this.props.history.push("./Update/" + id);
	}
	//#endregion

	renderSessionSynopsesTable(sessionSynopses) {
		return (
			<AuthContext.Consumer>
				{({ selectTask }) => (
					sessionSynopses.length === 0
						? <Table.Body>
							<Table.Row>
								<Table.Cell colSpan='6'>
									<Grid centered rows='2'>
										<br />
										<Grid.Row centered>
											<Button circular icon='plus' positive size="large" href="./SessionSynopsis/Create" />
										</Grid.Row>
										<Grid.Row centered>
											<Header icon as='h4' textAlign='center'>
												Add Your First Session Synopsis
                	</Header>
										</Grid.Row>
										<br />
									</Grid>
								</Table.Cell>
							</Table.Row>
						</Table.Body>
						: <Table.Body>
							{sessionSynopses.map((sessionSynopsis, index) => {
								return Math.ceil((index+1)/5) === parseInt(this.state.activePage) ?
									<Table.Row key={sessionSynopsis.id} warning={!sessionSynopsis.isVisible}>
										<Table.Cell>{sessionSynopsis.id}</Table.Cell>
										<Table.Cell>{sessionSynopsis.sessionSynopsisName}</Table.Cell>
										<Table.Cell>{sessionSynopsis.isVisible
											? <Icon name="check" />
											: <Icon name="close" />}
										</Table.Cell>
										<Table.Cell>{sessionSynopsis.createdBy}</Table.Cell>
										<Table.Cell>{sessionSynopsis.updatedBy}</Table.Cell>
										<Table.Cell>
											<Button.Group size="mini">
												<Button positive
													onClick={(event) => this.handleUpdateClick(event, sessionSynopsis.id, selectTask)}
												//href={"/SessionSynopsis/Update/" + sessionSynopsis.id}
												>Update</Button>
												<Button.Or />
												<Button negative
													onClick={this.deleteDialog.bind(this, sessionSynopsis)}>Delete</Button>
											</Button.Group>
										</Table.Cell>
									</Table.Row>
									:
									null
							}
							)}
						</Table.Body>
				)}
			</AuthContext.Consumer>
		);
	}

	render() {
		let contents = this.state.loading
			? <Table.Body>
				<Table.Row>
					<Table.Cell colSpan='6'>
						<Loader active inline='centered' />
					</Table.Cell>
				</Table.Row>
			</Table.Body>
			: this.renderSessionSynopsesTable(this.state.sessionSynopses);

		return (
			<div>
				<Header as='h2'>
					Manage Session Synopsis
                </Header>
				<Table celled size='small' sortable>
					<Table.Header>
						<Table.Row>
							<Table.HeaderCell colSpan='6'>
								<Grid columns='equal'>
									<Grid.Row>
										<Grid.Column>
											<Search size="mini"
												results={this.state.results}
												onSearchChange={this.handleSearchChange}
												onResultSelect={this.handleResultSelect} />
										</Grid.Column>
										<Grid.Column>
											<Button icon labelPosition='left' floated='right' positive size="small" href="./SessionSynopsis/Create">
												<Icon name='plus' />
												Add Session Synopsis
                            </Button>
										</Grid.Column>
									</Grid.Row>
								</Grid>
							</Table.HeaderCell>
						</Table.Row>
						<Table.Row>
							<Table.HeaderCell>#</Table.HeaderCell>
							<Table.HeaderCell
								sorted={this.state.column === 'sessionSynopsisName' ? this.state.direction : null}
								onClick={this.handleSort('sessionSynopsisName')}>Session Synopsis Name</Table.HeaderCell>
							<Table.HeaderCell
								sorted={this.state.column === 'isVisible' ? this.state.direction : null}
								onClick={this.handleSort('isVisible')}>Visibility</Table.HeaderCell>
							<Table.HeaderCell
								sorted={this.state.column === 'createdBy' ? this.state.direction : null}
								onClick={this.handleSort('createdBy')}>Created by</Table.HeaderCell>
							<Table.HeaderCell
								sorted={this.state.column === 'updatedBy' ? this.state.direction : null}
								onClick={this.handleSort('updatedBy')}>Updated by</Table.HeaderCell>
							<Table.HeaderCell>Actions</Table.HeaderCell>
						</Table.Row>
					</Table.Header>
					{contents}

					<Table.Footer>
						<Table.Row>
							<Table.HeaderCell colSpan='6'>
								<Pagination floated='right'
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
					header={'Are you sure you want to delete  ' + this.state.selectedSession.sessionSynopsisName + '?'}
					content='This action cannot be undone'
					onCancel={this.deleteDialog}
					onConfirm={this.deleteSessionSynopsis.bind(this, this.state.selectedSession.id)}
					size='mini'
				/>
			</div>

		);
	}
}
