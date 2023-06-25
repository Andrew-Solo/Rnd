import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import AccountContainer from "../account/AccountContainer";
import AppContainer from "../sidebar/AppContainer";
import {observer} from "mobx-react-lite";
import UnitPage from "../units/UnitPage";
import {store} from "../../stores/Store";
import PageLoader from "../ui/PageLoader";
import InstancePage from "../instances/InstancePage";

const AppRouter = observer(() => {
  const {loaded, data} = store.modules;

  if (!loaded) return (<PageLoader/>);

  const defaultModule = getDefaultModel(data)?.name ?? "";
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
          {createModuleRoutes(data)}
        </Route>
        <Route path="*" element={<Navigate to={defaultPath}/>}/>
      </Routes>
    </BrowserRouter>
  );
});

function createModuleRoutes(modules) {
  return modules.map(module =>
    <Route key={module.name} path={module.path}>
      {createUnitRoutes(module.units)}
    </Route>
  )
}

function createUnitRoutes(units) {
  const {loaded, data} = units;

  if (!loaded) return <Route index element={<PageLoader/>}/>;

  const defaultUnit = getDefaultModel(data);

  return (
    <>
      <Route index element={<Navigate to={`/app/${defaultUnit.path}`}/>}/>
      {data.map(unit =>
        <Route key={unit.name} path={unit.name}>
          <Route index element={<UnitPage unit={unit}/>}/>
          <Route path=":name" element={<InstancePage unit={unit}/>}/>
        </Route>
      )}
    </>
  );
}

function getDefaultModel(models) {
  return models.filter(model => model.default)[0] ?? models[0];
}

export default AppRouter