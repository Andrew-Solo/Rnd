import {Box, List, ListItemText, ListItemButton, ListItemIcon} from "@mui/material";
import Brand from "../../components/Brand";
import Account from "./components/Account";
import CurrentGame from "./components/CurrentGame";
import {Group, History, Home} from "@mui/icons-material";

export default function Sidebar () {
  return (
    <Box width="350px" display="flex" flexDirection="column" justifyContent="space-between">
      <Box display="flex" flexDirection="column">
        <CurrentGame/>
        <List component="nav">
          <ListItemButton>
            <ListItemIcon>
              <Home/>
            </ListItemIcon>
            <ListItemText>
              Игры
            </ListItemText>
          </ListItemButton>
          <ListItemButton>
            <ListItemIcon>
              <Group/>
            </ListItemIcon>
            <ListItemText>
              Персонажи
            </ListItemText>
          </ListItemButton>
          <ListItemButton>
            <ListItemIcon>
              <History/>
            </ListItemIcon>
            <ListItemText>
              Дакуродо
            </ListItemText>
          </ListItemButton>
        </List>
      </Box>
      <Box display="flex" flexDirection="column">
        <Brand/>
        <Account/>
      </Box>
    </Box>
  );
}