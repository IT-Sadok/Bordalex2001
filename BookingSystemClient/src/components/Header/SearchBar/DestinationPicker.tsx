import { useState } from "react";

const destinations = ["Kyiv", "Odesa", "Lviv", "Dnipro", "Kharkiv"];

export default function DestinationPicker() {
  const [isOpen, setIsOpen] = useState(false);
  const [destination, setDestination] = useState("");

  return (
    <>
      <div className="relative flex-1">
        <button
          type="button"
          onClick={() => setIsOpen((prev) => !prev)}
          className="w-full rounded-2xl px-3 py-2 text-left hover:bg-gray-100"
        >
          <span className="block text-xs font-semibold">Where</span>
          <span className="block truncate text-sm text-gray-500">
            {destination || "Anywhere"}
          </span>
        </button>

        {isOpen && (
          <div className="absolute left-0 top-full z-50 mt-4 w-80 rounded-2xl border border-gray-200 bg-white p-4 shadow-lg">
            <p className="mb-3 text-sm font-semibold">Search destinations</p>

            <input
              type="text"
              placeholder="Search destinations"
              className="w-full rounded-xl border border-gray-300 px-3 py-2 outline-none focus:border-gray-500"
            />

            <div className="mt-3">
              {destinations.map((destination) => (
                <button
                  key={destination}
                  type="button"
                  onClick={() => {
                    setDestination(destination);
                    setIsOpen(false);
                  }}
                  className="w-full rounded-xl px-3 py-2 text-left hover:bg-gray-100"
                >
                  {destination}
                </button>
              ))}
            </div>
          </div>
        )}
      </div>
    </>
  );
}
