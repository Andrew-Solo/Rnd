import {Box} from "@mui/material";
import ModuleHeader from "./ModuleHeader";

export default function ModuleContainer({children, ...props}) {
  return (
    <Box component="main" width={1} padding={4} gap={4} display="flex" flexDirection="column">
      <ModuleHeader {...props}/>
      {children}
    </Box>
  );
}