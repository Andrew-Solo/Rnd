﻿import {List} from "@mui/material";
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
  new NavItem("Дакуродо", History, "app/characters/00000000-0000-0000-0000-000000000000", "Вчера"),
  new NavItem("Авторизация", Home, "account/login"),
  new NavItem("Регистрация", Home, "account/register"),
];

function NavItem(title, icon, href, tip = null) {
  return {title, icon, href, tip};
}