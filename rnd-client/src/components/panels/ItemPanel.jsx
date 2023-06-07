import {Box} from "@mui/material";
import ItemHeader from "./ItemHeader";

export default function ItemPanel({children, ...props}) {
  return (
    <Box component="main" width={1} display="flex" flexDirection="column">
      <ItemHeader {...props}/>
      <Box padding={4}>
        {children}
      </Box>
    </Box>
  );
}