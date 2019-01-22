import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { NavMenu } from './components/NavMenu';
import { Home } from './components/Home';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { SignUp } from './components/SignUp';
import { CreateSessionSynopsis } from './components/SessionSynopsis/CreateSessionSynopsis';
import { ManageSessionSynopsis } from './components/SessionSynopsis/ManageSessionSynopsis';
import { UpdateSessionSynopsis } from './components/SessionSynopsis/UpdateSessionSynopsis';
import { CreateCustomerAccount } from './components/CustomerAccount/CreateCustomerAccount';
import { ManageCustomerAccount } from './components/CustomerAccount/ManageCustomerAccount';
import { UpdateCustomerAccount } from './components/CustomerAccount/UpdateCustomerAccount';
import { CreateAccountRate } from './components/AccountRate/CreateAccountRate';
import { UpdateAccountRate } from './components/AccountRate/UpdateAccountRate';
import { ManageAccountDetail } from './components/AccountDetail/ManageAccountDetail';
import { CreateAccountDetail } from './components/AccountDetail/CreateAccountDetail';
import AuthContext from './AuthContext';
import PrivateRoute from './PrivateRoute';

class AuthProvider extends Component {
  constructor(props) {
    super(props);
    if (localStorage.getItem('token') === null) {
      localStorage.setItem('token', this.state.token);
      localStorage.setItem('role', this.state.role);
    } else {
      if (localStorage.getItem('token').length !== 0) {
        this.state.token = localStorage.getItem('token');
        this.state.role = localStorage.getItem('role');
        this.state.isAuth = true;
      }
    }
  }
  state = {
    isAuth: false,
    token: '',
    role: '',
    updateAuth: (isAuth, token, role) => {
      this.setState({ isAuth: true, token: token, role: role }, function () {
        localStorage.setItem('token', token);
        localStorage.setItem('role', role)
      })
    },
    logout: () => {
      this.setState({ isAuth: false, token: '', role: '' }, function () {
        localStorage.setItem('token', this.state.token);
        localStorage.setItem('role', this.state.role);
      })
    },
    selectTask: (taskId) => {
      this.setState({ selectedTaskIdForUpdate: taskId })
    }
  }

  render() {
    return <AuthContext.Provider value={this.state}>
      {this.props.children}
    </AuthContext.Provider>
  }
}

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Router>
        <AuthProvider value={this.state}>
          <div>
            <PrivateRoute exact component={NavMenu} />
            <Switch>
              <Route exact path='/login' component={Login} />
              <Layout>
                <PrivateRoute exact path='/' component={Home} />
                {/* <Route exact path='/signup' component={SignUp} /> */}
                <PrivateRoute path='/SessionSynopsis/Manage' component={ManageSessionSynopsis} />
                <PrivateRoute path='/SessionSynopsis/Create' component={CreateSessionSynopsis} />
                <PrivateRoute path='/SessionSynopsis/Update' component={UpdateSessionSynopsis} />
                <PrivateRoute path='/CustomerAccount/Manage' component={ManageCustomerAccount} />
                <PrivateRoute path='/CustomerAccount/Create' component={CreateCustomerAccount} />
                <PrivateRoute path='/CustomerAccount/Update' component={UpdateCustomerAccount} />
                <PrivateRoute path='/CustomerAccount/AccountRate/Create' component={CreateAccountRate} />
                <PrivateRoute path='/CustomerAccount/AccountRate/Update' component={UpdateAccountRate} />
                <PrivateRoute path='/AccountDetail/Manage' component={ManageAccountDetail} />
                <PrivateRoute path='/AccountDetail/Create' component={CreateAccountDetail} />
              </Layout>
            </Switch>
          </div>
        </AuthProvider>
      </Router>
    );
  }
}
