import {Navigate, Route, Routes} from "react-router-dom";
import AccountContainer from "../account/AccountContainer";
import AppContainer from "../sidebar/AppContainer";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/StoreProvider";
import ModulePage from "../modules/ModulePage";
import UnitPage from "../units/UnitPage";

const LoggedRouter = observer(() => {
  const modules = useStore().modules.data;
  const defaultPath = `/app/${modules.filter(module => module.default)[0].name}`

  return (
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
  );
});

function createModuleRoutes(modules) {
  return modules.map(module =>
    <Route path={module.name}>
      <Route index element={<ModulePage/>}/>
      <Route path=":name" element={<UnitPage/>}/>
    </Route>
  )
}

export default LoggedRouter