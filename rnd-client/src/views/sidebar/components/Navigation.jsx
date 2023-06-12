import {List} from "@mui/material";
import NavigationItem from "./NavigationItem";
import {Group, Home, History} from "../../../components/icons";

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
  new NavItem("Игры", Home, "games"),
  new NavItem("Персонажи", Group, "characters"),
  new NavItem("Дакуродо", History, "/app/characters/@latest", "Вчера"),
  new NavItem("Авторизация", Home, "/account/login"),
  new NavItem("Регистрация", Home, "/account/register"),
];

function NavItem(title, icon, href, tip = null) {
  return {title, icon, href, tip};
}