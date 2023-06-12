import {Box, ListItemButton, ListItemIcon, ListItemText, Typography} from "@mui/material";
import {usePath} from "../../../hooks";
import Icon from "../../ui/Icon";

export default function NavigationItem ({module}) {
  const active = usePath(module.path);

  return (
    <ListItemButton key={module.path} href={`/app/${module.path}`} sx={{padding: 2, gap: 1, background: active ? "rgba(255, 255, 255, 0.1)" : null}}>
      <ListItemIcon sx={{minWidth: 0}}>
        <Icon icon={module.icon} color={active ? "primary" : "inherit"} sx={{marginTop: "-2px"}}/>
      </ListItemIcon>
      <ListItemText>
        <Box display="flex" justifyContent="space-between"  alignItems="flex-end">
          <Typography variant="h5" color={active ? "primary" : "inherit"}>
            {module.title}
          </Typography>
          <Typography variant="caption" color="text.secondary" align="right" sx={{mb: -0.25}}>
            {module.subtitle}
          </Typography>
        </Box>
      </ListItemText>
    </ListItemButton>
  );
}