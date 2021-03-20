import { useState } from 'react';
import LoginForm from './Components/LoginForm';

function App() {
  const adminUser = {
    email: "admin@admin.com",
    password: "admin123"
  };

  const [user, setUser] = useState({ name: "", email: "" });
  const [error, setError] = useState("");

  const Login = details => {
    console.log(details);
  }

  const Logout = () => {
    console.log("Logout");
  }
  return (
    <div className="App">
      <LoginForm login={Login} error={error} />
    </div>
  );
}

export default App;
