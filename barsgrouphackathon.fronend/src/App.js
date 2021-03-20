import { SnackbarProvider } from 'notistack';
import React, { useState } from 'react';
import Drawer from './Components/Drawer';
import LoginForm from './Components/Login';

function App() {
  const [login, setLogin] = useState("");
  return (
    <div className="App">
      <SnackbarProvider
        autoHideDuration={null}
        maxSnack={10}
        anchorOrigin={{ horizontal: 'right', vertical: 'top' }}
        preventDuplicate={true}>
        {!!login && (<Drawer />)}
        {!login && (<LoginForm setLogin={setLogin} />)}
      </SnackbarProvider >
    </div>
  );
}

export default App;
