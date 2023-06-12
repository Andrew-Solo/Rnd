import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import AccountContainer from "../account/AccountContainer";
import Login from "../account/Login";
import Register from "../account/Register";

const GuestRouter = () => {
  return (
    <BrowserRouter>
      <Routes>
        {/*TODO landing on / path*/}
        <Route index element={<Navigate to="/account/login"/>}/>
        <Route path="account" element={<AccountContainer/>}>
          <Route index element={<Navigate to="/account/login"/>}/>
          <Route path="login" element={<Login/>}/>
          <Route path="register" element={<Register/>}/>
        </Route>
        <Route path="*" element={<Navigate to="/account/login"/>}/>
      </Routes>
    </BrowserRouter>
  );
};

export default GuestRouter