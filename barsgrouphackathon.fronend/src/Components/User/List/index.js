
import { Typography } from '@material-ui/core';
import Box from '@material-ui/core/Box';
import IconButton from '@material-ui/core/IconButton';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import ListItemText from '@material-ui/core/ListItemText';
import AddIcon from '@material-ui/icons/Add';
import CheckIcon from '@material-ui/icons/Check';
import ClearIcon from '@material-ui/icons/Clear';
import FolderIcon from '@material-ui/icons/Folder';
import { useSnackbar } from 'notistack';
import React, { useEffect, useState } from 'react';
import { AddUser, DeleteFile, DeleteFilesFromBucket, DeleteUser, GetFiles, GetUsers } from '../../../Client';
import TextField from '../../Common/TextField';

function UserList() {
    const { enqueueSnackbar } = useSnackbar();
    const [activeUser, setActiveUser] = useState({ id: null });
    const [users, setUsers] = useState([]);
    const [userFiles, setUserFiles] = useState([]);
    const [userBucketFiles, setUserBucketFiles] = useState([]);
    const [isLoading, setLoading] = useState(false);
    const [isAddingUser, setAddingUser] = useState(false);
    const [userData, setUserData] = useState({ login: "", password: "" });


    useEffect(x => {
        setLoading(true);
        GetUsers()
            .then(users => {
                setUsers(users);
            })
            .catch(e => {

            })
            .finally(() => setLoading(false));
    }, []);

    const loadUsers = () => {
        setLoading(true);
        GetUsers()
            .then(users => {
                setUsers(users);
            })
            .catch(e => {

            })
            .finally(() => setLoading(false));
    }


    const handleLoadUserBucketFiles = (user) => {
        setLoading(true);
        GetFiles(user.id, true)
            .then(files => {
                setUserBucketFiles(files);
            })
            .catch(e => {

            })
            .finally(() => setLoading(false));
    }

    const handleLoadUserFiles = (user) => {
        setLoading(true);
        GetFiles(user.id)
            .then(files => {
                setUserFiles(files);
                handleLoadUserBucketFiles(user);
            })
            .catch(e => {

            })
            .finally(() => setLoading(false));
    }

    const handleDeleteUser = (user) => {
        setLoading(true);
        DeleteUser(user.id)
            .then(resp => {
                const index = users.indexOf(user);
                if (index > -1) {
                    var fs = users.map(x => x);
                    fs.splice(index, 1);
                    setUsers(fs);
                }
                setActiveUser({ id: null });
                setUserFiles([]);
                setUserBucketFiles([]);
                enqueueSnackbar("Пользователь удален", { variant: 'success' });
            })
            .catch(e => {
                enqueueSnackbar("Не удалось удалить пользователя", { variant: 'error' });
            })
            .finally(() => setLoading(false));
    }

    const handleDeleteUserFile = (file) => {
        DeleteFile(file.id)
            .then(resp => {
                const index = userFiles.indexOf(file);
                if (index > -1) {
                    var fs = userFiles.map(x => x);
                    fs.splice(index, 1);
                    setUserFiles(fs);
                    setUserBucketFiles(userBucketFiles.concat([file]));
                }
                enqueueSnackbar("Файл перемещен в корзину", { variant: 'success' });
            })
            .catch(e => {

            })
    }

    const handleDeleteUserFileFromBasket = (file) => {
        DeleteFilesFromBucket([file.id])
            .then(resp => {
                const index = userBucketFiles.indexOf(file);
                if (index > -1) {
                    var fs = userFiles.map(x => x);
                    fs.splice(index, 1);
                    setUserBucketFiles(fs);
                }
                enqueueSnackbar("Файл удален", { variant: 'success' });
            })
            .catch(e => {

            })
    }

    const handleSelectUser = user => {
        setActiveUser(user);
        handleLoadUserFiles(user);
    }

    const handleAddUser = userData => {
        if (!userData?.login || !userData?.password) {
            enqueueSnackbar("Логин и пароль не должны быть пустыми", { variant: 'info' });
            return;
        }

        AddUser(userData)
            .then(resp => {
                loadUsers();
                setUserData({ login: '', password: '' });
                setAddingUser(false);
                enqueueSnackbar("Пользователь добавлен", { variant: 'success' });
            })
            .catch(e => {
                enqueueSnackbar("Не удалось добавить пользователя", { variant: 'error' });
            })
    }

    const isActiveUser = (user) => user.id == activeUser.id;

    return (
        <Box display="flex" flex="1 1 0" flexDirection='column'>
            <Box display="flex" flexDirection="row" flex={1} justifyContent='flex-start' style={{ margin: 10 }}>
                <Box display='flex' flexDirection='column' alignItems="center" style={{ margin: 10 }}>
                    <Box flexDirection="row" display="flex" justifyContent="center">
                        <Typography align='center'>Пользователи</Typography>
                        {!isAddingUser && (<IconButton
                            style={{ padding: '0 0 0 10px' }}
                            onClick={() => setAddingUser(true)}>
                            <AddIcon />
                        </IconButton>)}
                    </Box>
                    <List style={{ minWidth: '350px' }}>
                        {users.map(user => {
                            const isActive = isActiveUser(user);
                            var backgroundColor = isActive ? 'rgba(0, 0, 0, 0.1)' : 'inherit';
                            return (
                                <ListItem style={{ backgroundColor: backgroundColor }}>
                                    <IconButton onClick={() => { handleDeleteUser(user); }}>
                                        <ClearIcon />
                                    </IconButton>
                                    <ListItemText primary={
                                        "Логин: " + user.login} secondary={`Файлов: ${user.fileNumber} Объем: ${user.dataSize} МБ`
                                        } />
                                    <ListItemSecondaryAction>
                                        <IconButton onClick={() => { handleSelectUser(user) }} disabled={isLoading}>
                                            <FolderIcon />
                                        </IconButton>
                                    </ListItemSecondaryAction>
                                </ListItem>
                            )
                        })}
                    </List>
                    {isAddingUser && (<Box flexDirection="column" display="flex" justifyContent="center">
                        <TextField
                            required
                            name="name"
                            label="Логин"
                            value={userData.login}
                            onChange={x => { setUserData({ ...userData, login: x }) }} />
                        <TextField
                            required
                            name="name"
                            type='password'
                            label="Пароль"
                            value={userData.password}
                            onChange={x => { setUserData({ ...userData, password: x }) }} />
                        <Box display={'flex'} flexDirection="row" alignItems='center' justifyContent='center'>
                            <IconButton edge="end" aria-label="delete" style={{ paddingBottom: 0 }}
                                onClick={() => { handleAddUser(userData); }} >
                                <CheckIcon fontSize='small' />
                            </IconButton>
                            <IconButton edge="end" aria-label="delete" style={{ paddingBottom: 0 }}
                                onClick={() => { setUserData({ login: '', password: '' }); setAddingUser(false); }} >
                                <ClearIcon fontSize='small' />
                            </IconButton>
                        </Box>

                    </Box>)}
                </Box>
                {userFiles.length > 0 && (<Box display='flex' flexDirection='column' alignItems="center" style={{ margin: 10 }}>
                    <Typography>Файлы {activeUser.login}</Typography>
                    <List>
                        {userFiles.map(file => {
                            const name = file.name?.length > 17 ? file.name.substring(0, 17).concat('...') : file.name;
                            return (
                                <ListItem>
                                    <ListItemText primary={name} secondary={(file.size / 1048576).toFixed(2) + " МБ"} />
                                    <ListItemSecondaryAction>
                                        <IconButton onClick={() => handleDeleteUserFile(file)} >
                                            <ClearIcon />
                                        </IconButton>
                                    </ListItemSecondaryAction>
                                </ListItem>
                            )
                        })}
                    </List></Box>)}
                {userBucketFiles.length > 0 && (<Box display='flex' flexDirection='column' alignItems="center" style={{ margin: 10 }}>
                    <Typography>Корзина {activeUser.login}</Typography>
                    <List>
                        {userBucketFiles.map(file => {
                            const name = file.name?.length > 17 ? file.name.substring(0, 17).concat('...') : file.name;
                            return (
                                <ListItem>
                                    <ListItemText primary={name} />
                                    <ListItemSecondaryAction>
                                        <IconButton onClick={() => handleDeleteUserFileFromBasket(file)} >
                                            <ClearIcon />
                                        </IconButton>
                                    </ListItemSecondaryAction>
                                </ListItem>
                            )
                        })}
                    </List>
                </Box>)}
            </Box>
        </Box >
    )
}

export default UserList;