import {Box, Stack} from "@mui/material";
import ItemHeader from "./ItemHeader";

export default function ItemPanel({children, data, actions, ...props}) {
  return (
    <Box component="main" width={1} display="flex" flexDirection="column">
      <ItemHeader {...props}/>
      <Box padding={4}>
        <Stack maxWidth={750} gap={4}>
          <Box display="flex" gap={4}>
            <Box width={7.5/10} minWidth={150}>
              {children[0]}
            </Box>
            <Box width={2.5/10} minWidth={100}>
              {children[1]}
            </Box>
          </Box>
          {children[2]}
        </Stack>
      </Box>
    </Box>
  );
}