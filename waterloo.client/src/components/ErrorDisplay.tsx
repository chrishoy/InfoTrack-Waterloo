import React from "react";
import { ErrorResponse } from "../types/errorTypes";

const ErrorDisplay = ({ error }: { error: ErrorResponse | null }) => {
    if (!error) return null;
    return (
        <div className="w-full rounded-lg border border-red-500 bg-red-100 p-4">
            <h2 className="font-bold text-red-700">{error.detail}</h2>
            <ul className="mt-2 list-inside list-disc text-red-600">
                {error.errors.map((err, index) => (
                    <li key={index}>{err.description}</li>
                ))}
            </ul>
        </div>
    );
};

export default ErrorDisplay;
