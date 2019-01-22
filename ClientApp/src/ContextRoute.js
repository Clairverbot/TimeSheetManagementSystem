import React from 'react';
import { Route } from 'react-router-dom';
import AuthContext from './AuthContext';
const ContextRoute = ({ component: Component, ...rest }) => (
    <AuthContext.Consumer>
        {({ isAuth }) => (
            <Route
                render={props =>
                    <Component {...props} isAuth={isAuth} />

                }
                {...rest}
            />
        )}
    </AuthContext.Consumer>
);

export default ContextRoute;