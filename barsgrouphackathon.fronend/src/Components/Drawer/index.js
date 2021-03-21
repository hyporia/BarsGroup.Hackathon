import { Box } from '@material-ui/core';
import AppBar from '@material-ui/core/AppBar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Divider from '@material-ui/core/Divider';
import Drawer from '@material-ui/core/Drawer';
import IconButton from '@material-ui/core/IconButton';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import { useTheme } from '@material-ui/core/styles';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import AccountBoxIcon from '@material-ui/icons/AccountBox';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import DeleteOutlineIcon from '@material-ui/icons/DeleteOutline';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import FolderIcon from '@material-ui/icons/Folder';
import MenuIcon from '@material-ui/icons/Menu';
import clsx from 'clsx';
import React from 'react';
import { BrowserRouter, Link, Redirect, Route, Switch } from 'react-router-dom';
import Bucket from '../Bucket';
import useStyles from '../Drawer/useStyles';
import FileList from '../File/List';
import UserList from '../User/List';

export default function MiniDrawer({ login, handleLogout, isAdmin }) {
    const classes = useStyles();
    const theme = useTheme();
    const [open, setOpen] = React.useState(false);

    const handleDrawerOpen = () => {
        setOpen(true);
    };

    const handleDrawerClose = () => {
        setOpen(false);
    };

    return (
        <BrowserRouter>
            <Box display='flex'>
                <CssBaseline />
                <AppBar
                    position="fixed"
                    className={clsx(classes.appBar, {
                        [classes.appBarShift]: open,
                    })}
                >
                    <Toolbar>
                        <IconButton
                            color="inherit"
                            aria-label="open drawer"
                            onClick={handleDrawerOpen}
                            edge="start"
                            className={clsx(classes.menuButton, {
                                [classes.hide]: open,
                            })}
                        >
                            <MenuIcon />
                        </IconButton>
                        <Typography variant="h6" noWrap>
                            {login}
                        </Typography>
                    </Toolbar>
                </AppBar>
                <Drawer
                    variant="permanent"
                    className={clsx(classes.drawer, {
                        [classes.drawerOpen]: open,
                        [classes.drawerClose]: !open,
                    })}
                    classes={{
                        paper: clsx({
                            [classes.drawerOpen]: open,
                            [classes.drawerClose]: !open,
                        }),
                    }}
                >
                    <div className={classes.toolbar}>
                        <IconButton onClick={handleDrawerClose}>
                            {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
                        </IconButton>
                    </div>
                    <Divider />
                    <List>
                        {isAdmin && (<Link to={'/users'} key={"Пользователи"} >
                            <ListItem button key={"Пользователи"}>
                                <ListItemIcon><AccountBoxIcon /></ListItemIcon>
                                <ListItemText primary={"Пользователи"} />
                            </ListItem>
                        </Link>)}
                        {!isAdmin && (<Link to={'/files'} key={"Файлы"} >
                            <ListItem button key={"Файлы"}>
                                <ListItemIcon><FolderIcon /></ListItemIcon>
                                <ListItemText primary={"Файлы"} />
                            </ListItem>
                        </Link>)}
                        {!isAdmin && (<Link to={'/bucket'} key={"Корзина"} >
                            <ListItem button key={"Корзина"}>
                                <ListItemIcon><DeleteOutlineIcon /></ListItemIcon>
                                <ListItemText primary={"Корзина"} />
                            </ListItem>
                        </Link>)}

                        <Divider style={{ marginBottom: '10px' }} />
                        <Link to={''} style={{ textDecoration: 'none', color: 'inherit' }} ><ListItem button onClick={handleLogout}>
                            <ListItemIcon><ExitToAppIcon /></ListItemIcon>

                            <ListItemText primary={"Выход"} />
                        </ListItem></Link>
                    </List>
                </Drawer>
                <Box display='flex' flexDirection='column' style={{ minHeight: '100vh' }} flex={1}>
                    <div className={classes.toolbar} />
                    <Switch>
                        {isAdmin && (
                            <Route exact path="/" render={() => (<Redirect to="/users" />)} />)}
                        {!isAdmin && (
                            <Route exact path="/" render={() => (<Redirect to="/files" />)} />)}
                        <Route name="Файлы" path="/files" exact={true} component={FileList} />
                        <Route name="Пользователи" path="/users" exact={true} component={UserList} />
                        <Route name="Корзина" path="/bucket" exact={true} component={Bucket} />
                        <Route path="*" render={() => (<Redirect to="/files" />)} />
                    </Switch>
                </Box>
            </Box>
        </BrowserRouter>
    );
}
