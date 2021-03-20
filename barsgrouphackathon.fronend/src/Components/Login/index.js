import Box from '@material-ui/core/Box';
import Button from '@material-ui/core/Button';
import FormControl from '@material-ui/core/FormControl';
import React, { useState } from 'react';
import { AuthorizeUser } from '../../Client';
import TextField from '../Common/TextField';

const LoginForm = ({ setLogin }) => {
    const [details, setDetails] = useState({ login: "", password: "" });
    const [error, setError] = useState("");

    const Login = details => {
        AuthorizeUser(details.login, details.password)
            .then(resp => {
                setLogin(details.login);
            });
    }

    const Logout = () => {
        console.log("Logout");
    }

    const submitHandler = () => {
        Login(details);
    }

    return (
        <Box display="flex" justifyContent="center" flex={1}>
            <Box display="flex" justifyContent="space-around">
                <FormControl>
                    {!!error && (<div>{error}</div>)}
                    <TextField
                        label="Логин"
                        name="displayName"
                        value={details.login}
                        onChange={x => setDetails({ ...details, login: x })} />
                    <TextField
                        label="Пароль"
                        name="displayName"
                        onChange={x => setDetails({ ...details, password: x })}
                        value={details.password}
                        type="password" />
                    <Button onClick={submitHandler}>Войти</Button>
                </FormControl>
            </Box>
        </Box>
    )
}

export default LoginForm;