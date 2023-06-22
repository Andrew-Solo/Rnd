import {List} from "@mui/material";
import NavigationGroup from "./NavigationGroup";
import {observer} from "mobx-react-lite";
import {store} from "../../../stores/Store";

const Navigation = observer(() => {
  const {loaded, failed, message, data} = store.modules;

  if (!loaded) return 'Loading...';
  if (failed) return message.title;

  return (
    <List component="nav" sx={{padding: 0}}>
      {data.map(module => (
        <NavigationGroup key={module.name} module={module}/>
      ))}
    </List>
  );
});

export default Navigation