import {List} from "@mui/material";
import ModuleNavigation from "./ModuleNavigation";
import {observer} from "mobx-react-lite";
import {store} from "../../../stores/Store";

const Navigation = observer(() => {
  const {loaded, failed, message, data} = store.modules;

  if (!loaded) return 'Loading...';
  if (failed) return message.title;

  return (
    <List component="nav" sx={{padding: 0}}>
      {data.map(module => (
        <ModuleNavigation key={module.name} module={module} units={store.createUnits(module.name)}/>
      ))}
    </List>
  );
});

export default Navigation