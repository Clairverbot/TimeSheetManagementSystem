import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import AuthContext from './AuthContext';
//Had a challenge to pass the token to a class component through the route technique
//https://til.hashrocket.com/posts/z8cimdpghg-passing-props-down-to-react-router-route
//https://youtu.be/By7vJuSPaYo

const PrivateRoute = ({ component: Component, ...rest }) => (
  <AuthContext.Consumer>
    {({ isAuth, token, selectedTaskIdForUpdate }) => (
      <Route
        render={props =>
          
          isAuth ? <Component token={token} selectedTaskIdForUpdate={selectedTaskIdForUpdate} {...props} /> : <Redirect to="/login" />
         
            
        }
        {...rest}
      />
    )}
  </AuthContext.Consumer>
);

export default PrivateRoute;
