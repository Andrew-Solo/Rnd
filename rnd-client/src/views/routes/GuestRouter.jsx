import {Navigate, Route, Routes} from "react-router-dom";
import Login from "views/account/Login";
import Register from "views/account/Register";
import AccountContainer from "views/account/AccountContainer";

const GuestRouter = () => {
  return (
    <Routes>
      {/*TODO landing on / path*/}
      <Route index element={<Navigate to="/account/login"/>}/>
      <Route path="account" element={<AccountContainer/>}>
        <Route index element={<Navigate to="/account/login"/>}/>
        <Route path="login" element={<Login/>}/>
        <Route path="register" element={<Register/>}/>
      </Route>
    </Routes>
  );
};

export default GuestRouter