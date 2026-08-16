import DropdownMenu from "./DropdownMenu/DropdownMenu";

export default function UserMenu() {
  return (
    <>
      <DropdownMenu
        trigger={
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 16 16"
            fill="currentColor"
            className="size-4"
          >
            <path
              fillRule="evenodd"
              d="M2 3.75A.75.75 0 0 1 2.75 3h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 3.75ZM2 8a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 8Zm0 4.25a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
              clipRule="evenodd"
            />
          </svg>
        }
        items={[
          { label: "Login", to: "/login" },
          { label: "Register", to: "/register" },
          { label: "Become a host", to: "#" },
          { label: "Help center", to: "#" }
        ]}
      />
    </>
  );
}
