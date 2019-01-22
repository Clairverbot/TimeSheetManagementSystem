import React, { Component } from 'react';
import AuthContext from './../../AuthContext'
import { Header, Form, Button, Checkbox } from 'semantic-ui-react';
import axios from 'axios';

export class UpdateSessionSynopsis extends Component {
	displayName = UpdateSessionSynopsis.name
	constructor(props) {
		super(props)

		this.state = {
			id: props.selectedTaskIdForUpdate,
			sessionSynopsisName: '',
			visibility: true
		}
		fetch('/api/SessionSynopses/' + this.state.id,{
			withCredentials: false,
            headers: {
                'Authorization': 'Bearer ' + this.props.token
            },
		})
			.then(response => response.json())
			.then(data => {
				this.setState({
					sessionSynopsisName: data.sessionSynopsisName,
					visibility: data.isVisible
				});
				console.log(data)
			});
		this.submitSessionSynopsisForm = this.submitSessionSynopsisForm.bind(this);

	}

	submitSessionSynopsisForm() {
		let error = false;
		if (this.state.sessionSynopsisName === '') {
			this.setState({ sessionSynopsisNameError: true })
			error = true
		} else {
			this.setState({ sessionSynopsisNameError: false })
			error = false
		}
		if (error) {
			//this.setState({ formError: true })
			return
		} else {
			//this.setState({ formError: false })
		}
		
		var config = {
			withCredentials: false,
		headers: {
			'Content-Type': 'multipart/form-data',
			'Authorization': 'Bearer ' + this.props.token } 
		}
		let bodyFormData = new FormData();
		bodyFormData.set('sessionSynopsisName',this.state.sessionSynopsisName);
		bodyFormData.set('isVisible', this.state.visibility === true ? true : false);
		axios.put(`/api/sessionSynopses/${this.state.id}`,  bodyFormData, config )
		.then(res=>{
			if(res.status===200){
				console.log(res)
				alert(res.data.message)
				this.props.history.push("../Manage");
			}
			else if(res.status===400){
				alert("Session Synopsis name already exist!")
			}
		})
	};

	render() {
		return (
			<div>
				<Header as='h2'>
					Update Session Synopsis
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
							checked={this.state.visibility}/>
					</Form.Field>

					<Button positive floated="right" onClick={this.submitSessionSynopsisForm}>Update</Button>
					<Button type='clear' floated="right">Cancel</Button>
				</Form>
			</div>

		);
	}
}
