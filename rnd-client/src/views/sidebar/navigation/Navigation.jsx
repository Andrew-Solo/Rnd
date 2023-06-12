import {List} from "@mui/material";
import NavigationItem from "./NavigationItem";
import {useStore} from "../../../stores/StoreProvider";
import {observer} from "mobx-react-lite";

const Navigation = observer(() => {
  const modules = useStore().modules.data;

  return (
    <List component="nav" sx={{padding: 0}}>
      {modules.map(module => (
        <NavigationItem module={module}/>
      ))}
    </List>
  );
});

export default Navigation