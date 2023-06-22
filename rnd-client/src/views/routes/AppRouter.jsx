import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom";
import AccountContainer from "../account/AccountContainer";
import AppContainer from "../sidebar/AppContainer";
import {observer} from "mobx-react-lite";
import UnitPage from "../units/UnitPage";
import {store} from "../../stores/Store";
import PageLoader from "../ui/PageLoader";

const AppRouter = observer(() => {
  const {loaded, failed, message, data} = store.modules;

  if (!loaded) return (<PageLoader/>);
  if (failed) return message.title;

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
  const {loaded, failed, message, data} = units;

  if (!loaded) return <Route index element={<PageLoader/>}/>;
  if (failed) return message.title;
  if (data.length < 1) return "Пустой модуль";

  const defaultUnit = getDefaultModel(data);
  const defaultElement = data.length > 1
    ? <Navigate to={`/app/${defaultUnit.path}`}/>
    : <UnitPage unit={defaultUnit}/>;

  return (
    <>
      <Route key={defaultUnit.name} index element={defaultElement}/>
      {data.map(unit =>
        <Route key={unit.name} path={unit.name} element={<UnitPage unit={unit}/>}/>
      )}
    </>
  );
}

function getDefaultModel(models) {
  return models.filter(model => model.default)[0] ?? models[0];
}

export default AppRouter