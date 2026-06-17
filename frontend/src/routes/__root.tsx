import { Outlet } from "@tanstack/react-router";
import { createRootRouteWithContext } from "@tanstack/react-router";

import { RouterContext } from "@/app/router/context";

export const Route =
  createRootRouteWithContext<RouterContext>()({
    component: RootLayout,
  });

function RootLayout() {
  return (
    <>
      <Outlet />
    </>
  );
}