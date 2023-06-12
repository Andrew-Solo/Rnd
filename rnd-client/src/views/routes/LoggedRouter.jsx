import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import AccountContainer from "../account/AccountContainer";
import AppContainer from "../sidebar/AppContainer";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/StoreProvider";
import ModulePage from "../modules/ModulePage";
import UnitPage from "../units/UnitPage";

const LoggedRouter = observer(() => {
  const modules = useStore().modules.data;
  const defaultModule = modules.filter(module => module.default)[0]?.name ?? modules[0].name ?? "";
  const defaultPath = `/app/${defaultModule}`

  return (
    <BrowserRouter>
      <Routes>
        {/*TODO landing on / path*/}
        <Route index element={<Navigate to={defaultPath} />}/>
        <Route path="account" element={<AccountContainer/>}>
          <Route index element={<Navigate to={defaultPath}/>}/>
          <Route path="signout"/>
        </Route>
        <Route path="app" element={<AppContainer/>}>
          <Route index element={<Navigate to={defaultPath}/>}/>
          {createModuleRoutes(modules)}
        </Route>
        <Route path="*" element={<Navigate to={defaultPath}/>}/>
      </Routes>
    </BrowserRouter>
  );
});

function createModuleRoutes(modules) {
  return modules.map(module =>
    <Route key={module.name} path={module.name}>
      <Route index element={<ModulePage key={module.name} module={module}/>}/>
      <Route path=":name" element={<UnitPage/>}/>
    </Route>
  )
}

export default LoggedRouter