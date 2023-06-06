import {List} from "@mui/material";
import {Group, Home, History} from "../../../components/Icons";
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
  new NavItem("Игры", Home, "app/games"),
  new NavItem("Персонажи", Group, "app/characters"),
  new NavItem("Дакуродо", History, "app/characters/00000000-0000-0000-0000-000000000000"),
  new NavItem("Авторизация", Home, "app/characters/00000000-0000-0000-0000-000000000000"),
  new NavItem("Регистрация", Home, "app/characters/00000000-0000-0000-0000-000000000000"),
];

function NavItem(title, icon, href) {
  return {title, icon, href};
}