import {List} from "@mui/material";
import NavigationItem from "./NavigationItem";
import {useStore} from "../../../stores/StoreProvider";
import {observer} from "mobx-react-lite";

const Navigation = observer(() => {
  const {loaded, failed, message, data} = useStore().modules;

  if (!loaded) return 'Loading...';
  if (failed) return message.title;

  return (
    <List component="nav" sx={{padding: 0}}>
      {data.map(module => (
        <NavigationItem key={module.name} module={module}/>
      ))}
    </List>
  );
});

export default Navigation