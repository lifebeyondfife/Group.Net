Group.Net
=========

A Group Theory Library in C#

Group Theory and Computational Algebra
====================================

Group Theory is part of the wider mathematical discipline of Algebra. If you want to measure and describe the number of occurrences of something, use an integer. If you want to measure and describe the amount of something, use a rational number. If you want to measure and describe the symmetry of a structure, use a group.

Essentially, group theory deals with symmetry - in both real world and hypothetical structures.

Haven't you heard of GAP?
-------------------------

For almost every group theory use case there could possibly be, your first port of call should be the open source programming library <a href="http://www.gap-system.org/">GAP</a>.

Reason for Group.Net
====================

The Group.Net library is written entirely in C# which makes it much more portable and interoperable with Microsoft platforms. It is also open source.

At present it doesn't have much functionality. Some capabilities include:

 - Create Symmetric, Dihedral and arbitrary permutation groups
 - Orbit calculation: pointwise orbits on (i) a partial set of integer points or (ii) a full image set of CLR objects
 - Stabiliser calculation: pointwise stabiliser.
 - Group optimisation to construct a minimal set of generators

Future Work
===========

The current implementations of some algorithms would make a group theorist blush e.g. finding the order of a group by calculating the orbit of a full set of points rather than creating a subgroup chain and coset representatives. I'm aware of the naivety and will aim to implement a Monte Carlo based Schreier-Sims algorithm when I have time. After that there will be variations of setwise implementations to go along with the current pointwise ones.

Everywhere has to start somewhere though.
