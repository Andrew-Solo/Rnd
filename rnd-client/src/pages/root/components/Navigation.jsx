import {List, ListItemButton, ListItemIcon, ListItemText, Typography} from "@mui/material";
import {GroupOutlined, HistoryOutlined, HomeOutlined} from "@mui/icons-material";
import {Link} from "react-router-dom";

export default function Navigation () {
  return (
    <List component="nav">
      {navItems.map(item => (
        <ListItemButton key={item.title} component={Link} to={item.to}>
          <ListItemIcon>
            {item.icon}
          </ListItemIcon>
          <ListItemText>
            {/* TODO встроить эту тайпографи в ListItemText */}
            {/* TODO уменьшить расстояние от иконки до текста */}
            <Typography variant="h5">
              {item.title}
            </Typography>
          </ListItemText>
        </ListItemButton>
      ))}
    </List>
  );
}

const navItems = [
  new NavItem("Игры", <HomeOutlined/>, "app/games"),
  new NavItem("Персонажи", <GroupOutlined/>, "app/characters"),
  new NavItem("Дакуродо", <HistoryOutlined/>, "app/characters"),
];

function NavItem(title, icon, to) {
  return {title, icon, to};
}