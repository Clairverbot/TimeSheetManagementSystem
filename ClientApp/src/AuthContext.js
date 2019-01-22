import React from 'react';



const state = {
    isAuth: false,
    token: '',
    role:''
};

const AuthContext = React.createContext({ isAuth: state.number, token: state.token, role:state.role }); //passing initial value

export default AuthContext;
