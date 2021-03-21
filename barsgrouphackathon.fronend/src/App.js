
import Cookies from 'js-cookie';
import { SnackbarProvider } from 'notistack';
import React, { useState } from 'react';
import { LogoutUser } from './Client';
import Drawer from './Components/Drawer';
import LoginForm from './Components/Login';

function App() {
  const [login, setLogin] = useState("");
  const [isAdmin, setAdmin] = useState(false);
  const setUserData = ({ login, isAdmin }) => {
    if (!!login) {
      Cookies.set('login', login, { expires: 7 });
    } else {
      Cookies.remove("login");
    }
    if (isAdmin) {
      Cookies.set('isAdmin', isAdmin, { expires: 7 });
    } else {
      Cookies.remove("isAdmin");
    }
    setLogin(login);
    setAdmin(isAdmin);
  }

  const logoutHandler = () => {
    LogoutUser()
      .then()
      .catch()
      .finally(setUserData({ login: '', isAdmin: false }));
  }
  return (
    <div className="App">
      <SnackbarProvider
        autoHideDuration={3000}
        maxSnack={3}
        anchorOrigin={{ horizontal: 'right', vertical: 'top' }}
        preventDuplicate={true}>
        {!!login && (<Drawer login={login} isAdmin={isAdmin} handleLogout={logoutHandler} />)}
        {!login && (<LoginForm setUserData={setUserData} setAdmin={setAdmin} />)}
      </SnackbarProvider >
    </div>
  );
}

export default App;
