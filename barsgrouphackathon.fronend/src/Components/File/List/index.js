
import Box from '@material-ui/core/Box';
import { useSnackbar } from 'notistack';
import React, { useState } from 'react';
import { SaveFile } from '../../../Client';
import FileUploadButton from '../../Common/FileUploadButton';

function FileList({ login, error }) {
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();


    const [files, setFiles] = useState([]);
    const handleSelectFile = file => {
        SaveFile(file)
            .then(resp => {
                enqueueSnackbar("Файл успешно загружен", { variant: 'success' });
            })
            .catch(e => {
                enqueueSnackbar("Ошибка при загрузке файла", { variant: 'error' });
                console.error(e);
            });
    }
    return (
        <Box display="flex" flex="1 1 0">
            <Box margin={2}>
                <FileUploadButton alignSelf="start" text="Загрузить файл" onFileSelected={handleSelectFile} />
            </Box>

            {files.map(file => (<Box>{file.name}</Box>))}
        </Box>
    )
}

export default FileList;