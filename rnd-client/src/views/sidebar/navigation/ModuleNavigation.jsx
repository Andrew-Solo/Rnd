import {Box, Collapse, List, ListItemButton, ListItemIcon, ListItemText, Typography} from "@mui/material";
import {usePath} from "../../../hooks";
import Icon from "../../ui/Icon";
import {observer} from "mobx-react-lite";
import {useState} from "react";
import {ExpandLess, ExpandMore} from "../../icons";

const ModuleNavigation = observer(({module, units}) => {
  const active = usePath(module.name);
  const expandable = units.data.length > 1;

  if (expandable) {
    const [open, setOpen] = useState(false);
    return (
      <>
        <ListItemButton key={module.name} onClick={() => setOpen(!open)} sx={{padding: 2, gap: 1, background: active ? "rgba(255, 255, 255, 0.1)" : null}}>
          <ListItemIcon sx={{minWidth: 0}}>
            <Icon icon={module.icon} color={active ? "primary" : "inherit"} sx={{marginTop: "-2px"}}/>
          </ListItemIcon>
          <ListItemText>
            <Box display="flex" justifyContent="space-between" alignItems="center">
              <Typography variant="h5" color={active ? "primary" : "inherit"}>
                {module.title}
              </Typography>
              <Typography variant="caption" color="text.secondary" align="right" sx={{mb: -0.25}}>
                {module.subtitle}
              </Typography>
            </Box>
          </ListItemText>
          {open ? <ExpandLess/> : <ExpandMore/>}
        </ListItemButton>
        <Collapse in={open} timeout="auto" unmountOnExit>
          <List component="div" disablePadding>
            <ListItemButton sx={{pl: 4}}>
              <ListItemIcon>
                <Icon icon={module.icon} sx={{marginTop: "-2px"}}/>
              </ListItemIcon>
              <ListItemText primary="Starred"/>
            </ListItemButton>
          </List>
        </Collapse>
      </>
    );
  } else {
    return (
      <ListItemButton key={module.name} href={module.path} sx={{padding: 2, gap: 1, background: active ? "rgba(255, 255, 255, 0.1)" : null}}>
        <ListItemIcon sx={{minWidth: 0}}>
          <Icon icon={module.icon} color={active ? "primary" : "inherit"} sx={{marginTop: "-2px"}}/>
        </ListItemIcon>
        <ListItemText>
          <Box display="flex" justifyContent="space-between" alignItems="center">
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
});



export default ModuleNavigation