
import Cookies from 'js-cookie';
import { SnackbarProvider } from 'notistack';
import React, { useState } from 'react';
import { LogoutUser } from './Client';
import Drawer from './Components/Drawer';
import LoginForm from './Components/Login';

function App() {
  const [login, setLogin] = useState("");
  const setUserLogin = (login) => {
    if (!!login) {
      Cookies.set('login', login, { expires: 7 });
    } else {
      Cookies.remove("login");
    }
    setLogin(login);
  }

  const logoutHandler = () => {
    LogoutUser()
      .then()
      .catch()
      .finally(setUserLogin(''));
  }
  return (
    <div className="App">
      <SnackbarProvider
        autoHideDuration={3000}
        maxSnack={3}
        anchorOrigin={{ horizontal: 'right', vertical: 'top' }}
        preventDuplicate={true}>
        {!!login && (<Drawer login={login} handleLogout={logoutHandler} />)}
        {!login && (<LoginForm setLogin={setUserLogin} />)}
      </SnackbarProvider >
    </div>
  );
}

export default App;
