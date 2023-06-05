import {List} from "@mui/material";
import {GroupOutlined, HistoryOutlined, HomeOutlined} from "@mui/icons-material";
import NavigationItem from "./NavigationItem";

export default function Navigation () {
  return (
    <List component="nav" sx={{padding: 0}}>
      {navItems.map(item => (
        <NavigationItem {...item}/>
      ))}
    </List>
  );
}

const navItems = [
  new NavItem("Игры", HomeOutlined, "app/games"),
  new NavItem("Персонажи", GroupOutlined, "app/characters"),
  new NavItem("Дакуродо", HistoryOutlined, "app/characters/00000000-0000-0000-0000-000000000000"),
];

function NavItem(title, icon, href) {
  return {title, icon, href};
}