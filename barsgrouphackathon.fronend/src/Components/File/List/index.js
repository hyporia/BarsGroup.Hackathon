
import Box from '@material-ui/core/Box';
import React, { useState } from 'react';

function FileList({ login, error }) {
    const [details, setDetails] = useState({ login: "", password: "" });

    const submitHandler = () => {
        login(details);
    }
    return (
        <Box display="flex" flex="1 1 0" justifyContent="center">
            файлы
        </Box>
    )
}

export default FileList;