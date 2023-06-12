import {Box, ListItemButton, ListItemIcon, ListItemText, Typography} from "@mui/material";
import {usePath} from "../../../hooks";

export default function NavigationItem ({href, icon, title, tip}) {
  const active = usePath(href);
  const Icon = icon;

  return (
    <ListItemButton key={href} href={`/app/${href}`} sx={{padding: 2, gap: 1, background: active ? "rgba(255, 255, 255, 0.1)" : null}}>
      <ListItemIcon sx={{minWidth: 0}}>
        <Icon color={active ? "primary" : "inherit"} sx={{marginTop: "-2px"}}/>
      </ListItemIcon>
      <ListItemText>
        <Box display="flex" justifyContent="space-between"  alignItems="flex-end">
          <Typography variant="h5" color={active ? "primary" : "inherit"}>
            {title}
          </Typography>
          <Typography variant="caption" color="text.secondary" align="right" sx={{mb: -0.25}}>
            {tip}
          </Typography>
        </Box>
      </ListItemText>
    </ListItemButton>
  );
}