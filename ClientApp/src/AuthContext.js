import React from 'react';



const state = {
    isAuth: false,
    token: '',
    role:'',
    fullname:''
};

const AuthContext = React.createContext({ isAuth: state.number, token: state.token, role:state.role,fullname:state.fullname }); //passing initial value

export default AuthContext;
