// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;

contract SkinOwnership {
    address public owner;

    struct SkinOwner {
        address walletAddress;
        uint256[] skinIds;
    }
}

// contract address:
