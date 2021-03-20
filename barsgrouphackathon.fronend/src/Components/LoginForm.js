
import Box from '@material-ui/core/Box';
import Button from '@material-ui/core/Button';
import FormControl from '@material-ui/core/FormControl';
import React, { useState } from 'react';
import TextField from './Common/TextField';

function LoginForm({ login, error }) {
    const [details, setDetails] = useState({ username: "", password: "" });

    const submitHandler = e => {
        e.preventDefault();
        login(details);
    }
    return (
        <Box display="flex" flex="1 1 0" justifyContent="center">
            <FormControl>
                {!!error && (<div>{error}</div>)}
                <TextField
                    label="Логин"
                    name="displayName"
                    value={details.username}
                    onChange={x => setDetails({ ...details, username: x })} />
                <TextField
                    label="Пароль"
                    name="displayName"
                    onChange={x => setDetails({ ...details, password: x })}
                    value={details.password}
                    type="password" />
                <Button onClick={submitHandler}>Войти</Button>
            </FormControl>
        </Box>
    )
}

export default LoginForm